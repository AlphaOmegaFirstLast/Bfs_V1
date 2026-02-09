
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Bfs_Auth_Api>("bfs-auth-api");
builder.AddProject<Projects.Bfs_StockEx_Api>("bfs-stockex-api");

// Backend project (Identity.Web). public port 5001 maps to container port 5000
var identityWeb = builder.AddProject<Projects.Bfs_Identity_Web>("bfs-identity-web");
//   .WithHttpsEndpoint(port: 5001, targetPort: 5000)

// Frontend Angular app (dev-time "ng serve"). method AddExecutable runs it in its own process since is not .Net
var angular = builder.AddExecutable("frontend", "npm", "../../../frontend/main", "start")
    //.WithReference(identityWeb)      // link it to backend
    .WithHttpEndpoint(targetPort: 4200);   // expose to http://localhost


var gateway = builder.AddProject<Projects.Aspire_Gateway>("gateway")
    .WithReference(identityWeb)
    .WithEnvironment("AuthServiceUrl", identityWeb.GetEndpoint("http"))
    .WithEnvironment("MainServiceUrl", angular.GetEndpoint("http"));
builder.Build().Run();
