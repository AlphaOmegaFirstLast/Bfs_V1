using Bfs.Auth.Api.Validators;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data.Lists;
using Bfs.Auth.Data.Reports;
using Bfs.Auth.Data.Repositories;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Auth.Domain.Services;
using Bfs.Core.Auth;
using Bfs.Core.Config;
using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Core.Middleware;
using Bfs.Core.Services.Auth;
using Bfs.Core.TenantManagement;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bfs.Auth.Api;

public static class BuilderExtensions
{
    public static void RegisterScopeData(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IScopeData, ScopeData>();
    }

    public static void RegisterCrossOrigin(this WebApplicationBuilder builder, BfsSettings? settings)
    {

        if ((settings?.AllowedOrigins == null) || (settings?.AllowedOrigins == ""))
        {
            builder.Services.AddCors(o => o.AddPolicy("CrossOriginPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
        }
        else
        {
            var allowedOrigins = settings?.AllowedOrigins.Split(";");
            allowedOrigins = allowedOrigins?.Select(x => x.Trim()).ToArray();
            builder.Services.AddCors(o => o.AddPolicy("CrossOriginPolicy", builder =>
            {
                builder.WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
        }
    }

    public static void RegisterSecurity(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.IsSecurityEnabled)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = TokenService.TokenValidationParameters(settings.JwtSettings);
            });
        }
        else
        {
            builder.Services.AddAuthentication(options =>
            {
                // Optionally set your custom scheme as the default
                options.DefaultAuthenticateScheme = "NoSecurityScheme";
            })
            // Register the CustomScheme (using the handler and options defined above)
            .AddScheme<NoSecuritySchemeOptions, NoSecuritySchemeHandler>("NoSecurityScheme", options =>
            {
                // Configuration options can go here
            });
        }

        // Add Authorization
        builder.Services.AddAuthorization();

        // Register Authorization handlers and policy provider. that can handle dynamic policies.
        // IAuthorizationHandler is scoped because it is dependent on the current user's claims, which are evaluated per request. The handler needs to access the HttpContext to retrieve these claims,
        // and since HttpContext is scoped to the request, the handler must also be scoped to ensure it operates within the correct context.
       
        builder.Services.AddScoped<IAuthorizationHandler, MultiClaimRequirementHandler>();

        // The IAuthorizationPolicyProvider is registered as a singleton because it is responsible for providing authorization policies based on the current user's claims. It does not directly depend on the HttpContext or any per-request data,
        // but it needs to be available throughout the application's lifetime to evaluate policies for incoming requests. By registering it as a singleton,
        // we ensure that there is only one instance of the policy provider that can efficiently serve all requests without needing to be recreated for each one.
       
        builder.Services.AddSingleton<IAuthorizationPolicyProvider, DynamicAuthorizationPolicyProvider>();
    }

    public static void RegisterTenentRelated(this WebApplicationBuilder builder)
    {
        // Memory cache (app-wide)
        builder.Services.AddMemoryCache();

        // Permission cache (app-wide), PermissionProvider is a singleton because it is designed to cache permissions entry per Tenant
        builder.Services.AddSingleton<IPermissionProvider, PermissionProvider>();

        // Tenant resolution (per request). TenantProvider is scoped (via AddScoped) and can safely use HttpContext.
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ITenantProvider, TenantProvider>();
    }

    public static void RegisterDbContext(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        //This is the standard multi‑tenant pattern for database-per-tenant.
        // DbContext with dynamic connection string based on the current tenant
        builder.Services.AddDbContext<AuthDbContext>((serviceProvider, options) =>
        {
            var tenantProvider = serviceProvider.GetRequiredService<ITenantProvider>();
            var connectionString = tenantProvider.GetCurrentTenantDbConnection();
            options.UseSqlServer(connectionString, sql => { sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);});
        });
    }

    public static void RegisterValidators(this WebApplicationBuilder builder)
    {
            builder.Services.AddScoped<IValidator<AuthRoleComponentSystemAction>, AuthRoleComponentSystemActionValidator>();
            builder.Services.AddScoped<IValidator<AuthUser>, AuthUserValidator>();
            builder.Services.AddScoped<IValidator<AuthApp>, AuthAppValidator>();
            builder.Services.AddScoped<IValidator<AuthRole>, AuthRoleValidator>();
            builder.Services.AddScoped<IValidator<AuthRoleApp>, AuthRoleAppValidator>();
            builder.Services.AddScoped<IValidator<Contracts.AuthRoleUser>, AuthRoleUserValidator>();
//Template_Component_RegisterValidator
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAuthRoleComponentSystemActionRepository, AuthRoleComponentSystemActionRepository>();
            builder.Services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            builder.Services.AddScoped<IAuthAppRepository, AuthAppRepository>();
            builder.Services.AddScoped<IAuthRoleRepository, AuthRoleRepository>();
            builder.Services.AddScoped<IAuthRoleAppRepository, AuthRoleAppRepository>();
            builder.Services.AddScoped<IAuthRoleUserRepository, AuthRoleUserRepository>();
//Template_Component_RegisterRepository
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReportsService, ReportsService>();
        builder.Services.AddScoped<IOperationsService,OperationsService>();
            builder.Services.AddScoped<IAuthRoleComponentSystemActionService, AuthRoleComponentSystemActionService>();
            builder.Services.AddScoped<IAuthUserService, AuthUserService>();
            builder.Services.AddScoped<IAuthAppService, AuthAppService>();
            builder.Services.AddScoped<IAuthRoleService, AuthRoleService>();
            builder.Services.AddScoped<IAuthRoleAppService, AuthRoleAppService>();
            builder.Services.AddScoped<IAuthRoleUserService, AuthRoleUserService>();
//Template_Component_RegisterService
    }

    public static void RegisterLists(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.AuthConnection;
            builder.Services.AddScoped<IAuthRoleComponentSystemActionList>(provider =>
            {
                var tenantProvider = provider.GetRequiredService<ITenantProvider>();
                var dbConnection = tenantProvider.GetCurrentTenantDbConnection();
                return new AuthRoleComponentSystemActionList(dbConnection);
            });

            builder.Services.AddScoped<IAuthUserList>(provider =>
            {
                var tenantProvider = provider.GetRequiredService<ITenantProvider>();
                var dbConnection = tenantProvider.GetCurrentTenantDbConnection();
                return new AuthUserList(dbConnection);
            });

            builder.Services.AddScoped<IAuthAppList>(provider =>
            {
                var tenantProvider = provider.GetRequiredService<ITenantProvider>();
                var dbConnection = tenantProvider.GetCurrentTenantDbConnection();
                return new AuthAppList(dbConnection);
            });

            builder.Services.AddScoped<IAuthRoleList>(provider =>
            {
                var tenantProvider = provider.GetRequiredService<ITenantProvider>();
                var dbConnection = tenantProvider.GetCurrentTenantDbConnection();
                return new AuthRoleList(dbConnection);
            });

            builder.Services.AddScoped<IAuthRoleAppList>(provider =>
            {
                var tenantProvider = provider.GetRequiredService<ITenantProvider>();
                var dbConnection = tenantProvider.GetCurrentTenantDbConnection();
                return new AuthRoleAppList(dbConnection);
            });

            builder.Services.AddScoped<IAuthRoleUserList>(provider =>
            {
                var tenantProvider = provider.GetRequiredService<ITenantProvider>();
                var dbConnection = tenantProvider.GetCurrentTenantDbConnection();
                return new AuthRoleUserList(dbConnection);
            });

//Template_Component_RegisterList
        }
    }

    public static void RegisterReports(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.AuthConnection;
            builder.Services.AddScoped<IRoleRepCompare>(provider =>
            {
                var tenantProvider = provider.GetRequiredService<ITenantProvider>();
                var dbConnection = tenantProvider.GetCurrentTenantDbConnection();
                return new RoleRepCompare(dbConnection);
            });

//Template_Component_RegisterReport
        }
    }

    public static void RegisterClients(this WebApplicationBuilder builder, BfsSettings? settings)
    {
//Template_Component_RegisterHttpClient
    }
}
