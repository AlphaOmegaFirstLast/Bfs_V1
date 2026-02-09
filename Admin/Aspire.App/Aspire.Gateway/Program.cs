using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);
var authUrl = @"http://localhost:5101"; // builder.Configuration["AuthServiceUrl"];
var mainUrl = @"http://localhost:4200"; // builder.Configuration["MainServiceUrl"];

// Add YARP with custom config provider
builder.Services.AddReverseProxy()
    .LoadFromMemory(GetRoutes(), GetClusters(authUrl, mainUrl));

var app = builder.Build();
//app.Use(async (context, next) =>
//{
//    var path = context.Request.Path.Value;
//    if (path != null && (
//        path.StartsWith("/@fs") ||
//        path.StartsWith("/@vite") ||
//        path.StartsWith("/node_modules") ||
//        path.StartsWith("/__vite_ping")))
//    {
//        context.Response.StatusCode = 404;
//        await context.Response.WriteAsync("Blocked Vite internal path");
//        return;
//    }
//    await next();
//});

app.MapReverseProxy();
app.MapGet("/gateway", () => { return $"Hello. auth= {authUrl}  main= {mainUrl} "; });
app.Run();

static IReadOnlyList<RouteConfig> GetRoutes() => new[]
{
    new RouteConfig
    {
        RouteId = "manageRoute",
        ClusterId= "authCluster",
        Match= new RouteMatch {Path= "/auth/Identity/Account/Manage"}
    }
    ,
    new RouteConfig
    {
        RouteId = "loginRoute",
        ClusterId= "authCluster",
        Match= new RouteMatch {Path= "/auth/Identity/Account/Login"}
    }
    ,
    new RouteConfig
    {
        RouteId = "logoutRoute",
        ClusterId= "authCluster",
        Match= new RouteMatch {Path= "/auth/Identity/Account/Logout"}
    }
    ,
    new RouteConfig
    {
        RouteId = "authRoute",
        ClusterId = "authCluster",
        Match = new RouteMatch { Path = "/auth/{**catch-all}" }
    },
    new RouteConfig
    {
        RouteId = "mainRoute",
        ClusterId = "mainCluster",
        Match = new RouteMatch
        {
            Path = "/main/{**catch-all}"
        },
        Transforms = new[]
        {
            new Dictionary<string, string>
            {
                { "PathRemovePrefix", "/main" }
            }
        }
    }
    ,
    new RouteConfig
    {
        RouteId = "viteFsRoute",
        ClusterId = "mainCluster",
        Match = new RouteMatch
        {
            Path = "/@fs/{**catch-all}"
        }
    }
    ,
    new RouteConfig
    {
        RouteId = "viteNodeModulesRoute",
        ClusterId = "mainCluster",
        Match = new RouteMatch
        {
            Path = "/node_modules/{**catch-all}"
        }
    },
    new RouteConfig
    {
        RouteId = "viteIdRoute",
        ClusterId = "mainCluster",
        Match = new RouteMatch
        {
            Path = "/@id/{**catch-all}"
        }
    }
    ,
    new RouteConfig
    {
        RouteId = "viteInternalRoute",
        ClusterId = "mainCluster",
        Match = new RouteMatch
        {
            Path = "/@vite/{**catch-all}"
        }
    }
    ,
    new RouteConfig
    {
        RouteId = "vitePingRoute",
        ClusterId = "mainCluster",
        Match = new RouteMatch
        {
            Path = "/__vite_ping"
        }
    }
    ,
    new RouteConfig
        {
        RouteId = "chunkRoute",
        ClusterId = "mainCluster",
        Match = new RouteMatch
        {
        Path = "/{file}"
        }
    }
    ,
    //new RouteConfig
    //    {
    //    RouteId = "2SegmentsRoute",
    //    ClusterId = "mainCluster",
    //    Match = new RouteMatch
    //    {
    //    Path = "/{segment1}/{segment2}"
    //    }
    //}
    //,
    new RouteConfig
    {
        RouteId = "defaultRoute",
        ClusterId = "mainCluster",
        Match = new RouteMatch { Path = "/{**catch-all}" }
    }
};

static IReadOnlyList<ClusterConfig> GetClusters(string? authUrl, string? mainUrl) => new[]
{
    new ClusterConfig
    {
        ClusterId = "authCluster",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            ["auth"] = new DestinationConfig { Address = authUrl }
        }
    },
    new ClusterConfig
    {
        ClusterId = "mainCluster",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            ["main"] = new DestinationConfig { Address = mainUrl }
        }
    }
};
