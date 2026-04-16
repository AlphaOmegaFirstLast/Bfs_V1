//using Microsoft.ApplicationInsights.AspNetCore
using Bfs.Core.Config;
using Bfs.Core.Services.Diagnosis;
using Bfs.Core.TenantManagement;
using Microsoft.AspNetCore.Diagnostics;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Threading.Tasks;
using Bfs.Master.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var settings = builder.Configuration.GetSection("Settings").Get<BfsSettings>();
        builder.Services.Configure<BfsSettings>(builder.Configuration.GetSection("Settings"));

        // Add Services to the DI container. Configure Services + Register Dependency Injection (DI) for Services
        builder = ConfigureServices(builder, settings);

        // Generate WebApplication
        var app = builder.Build();

        // Build Request\Response Pipeline
        app = BuildApplicationPipeline(app, settings);

        // Run WebApplication
        app.Run();
    }

    private static WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder, BfsSettings? settings)
    {

        // Add Aspire support for development. Bfs.Core.AspireExtensions
        builder.AddServiceDefaults();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>   // Config Json serialization
            {
                // Allow reading numbers from strings globally
                options.JsonSerializerOptions.NumberHandling =
                    System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
            }); 

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.RegisterScopeData();
        builder.RegisterCrossOrigin(settings);
        builder.RegisterSecurity(settings);
        builder.RegisterTenentRelated();
        builder.RegisterDbContext(settings);
        builder.RegisterRepositories();
        builder.RegisterServices();
        builder.RegisterValidators();
        builder.RegisterLists(settings);
        builder.RegisterReports(settings);
        // This is added specifically to force Aspire to wait until the application is ready before processing requests, ensuring that all services are properly initialized.
        builder.Services.AddHealthChecks().AddCheck<ReadinessHealthCheck>("readiness");

        return builder;
    }

    private static WebApplication BuildApplicationPipeline(WebApplication app, BfsSettings? settings)
    {
        // Middleware pipeline logging
        app.Use(async (context, next) =>
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            using (logger.BeginScope("[Master]RequestPath: {Path}", context.Request.Path))
            {
                logger.LogInformation($"Handling request {context.Request.Path}");
                await next.Invoke();
                if (context.Response.StatusCode >= 400)
                {
                    logger.LogError($"Response.StatusCode: {context.Response.StatusCode}, {context.Response.Headers}");
                }
            }
        });
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context => await ApplicationErrorHandler(context));
        });

        // Add Aspire support for development. Bfs.Core.AspireExtensions
        app.MapDefaultEndpoints();

        //app.UseHttpsRedirection(); // Redirect HTTP → HTTPS
        app.UseStaticFiles(); // Serve static files
        app.UseRouting();
        app.UseCors("CrossOriginPolicy");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Master V1");
            c.DocExpansion(DocExpansion.None); // Collapses all nodes
        });

        TenantManager.LoadTenants(app, settings?.DbConnections?.MasterConnection);
        ReadinessHealthCheck.MarkReady();
        return app;
    }

    private static async Task ApplicationErrorHandler(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        var response = new
        {
            Message = "An unexpected error occurred.",
            Detail = exception?.Message,
            context.Request.Path
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
