//using Bfs.Auth.Client;
using Bfs.Core.Config;
using Bfs.Core.Services.Auth;
using Bfs.Identity.Web;
using Bfs.Identity.Web.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
//using NuGet.Protocol;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var settings = builder.Configuration.GetSection("Settings").Get<BfsSettings>();
        builder.Services.Configure<BfsSettings>(builder.Configuration.GetSection("Settings"));

        // Add Aspire support for development. Bfs.Core.AspireExtensions
        builder.AddServiceDefaults(); 
        builder.RegisterDbContext(settings);
        builder.RegisterScopeData();
        builder.RegisterIdentity<ApplicationDbContext>();
        builder.RegisterServices();
        builder.RegisterHttpClients(settings);
        builder.Services.AddLogging();

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddRazorPages();
        builder.Services.AddControllers();

        // Add HttpClient factory
        builder.Services.AddHttpClient();

        // Add cookie policy if needed
        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.Strict;
            options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
            options.Secure = CookieSecurePolicy.Always;
        });

        var app = builder.Build();
        // Middleware pipeline logging
        app.Use(async (context, next) =>
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            using (logger.BeginScope("[Identity]RequestPath: {Path}", context.Request.Path))
            {
                logger.LogInformation($"Handling request {context.Request.Path}");
                await next.Invoke();
                if (context.Response.StatusCode >= 400)
                {
                    logger.LogError($"Response.StatusCode: {context.Response.StatusCode}, {context.Response.Headers}");
                }
            }
        });

        app.UsePathBase("/auth");
        // Add Aspire support for development. Bfs.Core.AspireExtensions
        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.MapStaticAssets();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        // Map Razor Pages and API Controllers
        app.MapRazorPages()
           .WithStaticAssets();
        app.MapControllers();

        //if (app.Environment.IsDevelopment())
        //{
        //    var logger = app.Services.GetRequiredService<ILogger<Program>>();

        //    app.MapWhen(
        //        ctx => {
        //            var ok = 
        //            !ctx.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase)
        //         && !ctx.Request.Path.StartsWithSegments("/Identity", StringComparison.OrdinalIgnoreCase);

        //            logger.LogInformation("SPA Proxy: {Path} => {Proxy}", ctx.Request.Path, ok ? "Yes" : "No");
        //            return ok;
        //        }
        //        ,
        //        spaApp =>
        //        {
        //            logger.LogInformation("Configuring SPA proxy for unhandled request.");

        //            // Proxy unhandled requests to Angular dev server
        //            spaApp.UseSpa(spa =>
        //            {
        //                spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
        //            });
        //        });
        //}
        //else
        //{
        //    // For production, serve prebuilt Angular files
        //    //app.UseSpaStaticFiles();
        //    //app.UseSpa(spa => { spa.Options.SourcePath = "ClientApp"; });
        //}
        app.Run();
    }
}