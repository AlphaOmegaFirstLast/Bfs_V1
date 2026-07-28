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
using Bfs.Core.Services.Auth;
using Bfs.Core.Services.Security;
using Bfs.Core.TenantManagement;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Bfs.Auth.Api;

public static class BuilderExtensions
{
    public record TenantSqlConfiguration(string ConnectionString);

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

    public static void RegisterTenantRelated(this WebApplicationBuilder builder)
    {
        // Memory cache (app-wide)
        builder.Services.AddMemoryCache();

        // Permission cache (app-wide), PermissionProvider is a singleton because it is designed to cache permissions entry per Tenant
        builder.Services.AddSingleton<IPermissionProvider, PermissionProvider>();

        // Tenant resolution (per request). TenantProvider is scoped (via AddScoped) and can safely use HttpContext.
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ITenantManager, TenantManager>();
        builder.Services.AddScoped<IUserTenantService, UserTenantService>(); // needed only for Auth Api

        builder.Services.AddHostedService<CacheWarmingService>();  // AddHostedService so the host is going to manage the lifecycle of the CacheWarmingService, starting it when the application starts and stopping it when the application shuts down. This ensures that the background service runs continuously in the background, refreshing the tenant cache at regular intervals as defined in the CacheWarmingService implementation.

        // Get the current tenant's connection string from the TenantProvider and create a TenantSqlConfiguration that can be used by the lists to access the database.
        // This allows the lists to be tenant-aware and operate on the correct database for each request.
        // AddScoped is used here because the TenantProvider is scoped to the request, and we want to ensure that each list gets the correct connection string for the current tenant during that request. By registering TenantSqlConfiguration as scoped,
        //  we can safely inject it into the lists without worrying about cross-tenant data access issues.
        // ------------------------------------------------------------
        builder.Services.AddScoped(sp =>
        {
            var tenantProvider = sp.GetRequiredService<ITenantManager>();
            var connectionString = tenantProvider.GetTenantDbConnection();
            return new TenantSqlConfiguration(connectionString);
        });

        // Resource Security
        builder.Services.AddScoped<IResourceSecurity, ResourceSecurity>();
        builder.Services.AddScoped<ITenantResourceRuleListItem, TenantResourceRuleListItem>();
        builder.Services.AddScoped<ITenantResourceRuleListFilter, TenantResourceRuleListFilter>();
        builder.Services.AddScoped<ITenantResourceRuleList>(sp =>
        {
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new TenantResourceRuleList(config.ConnectionString);
        });
    }

    public static void RegisterDbContext(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.IsMigrationEnabled)
        {
            // Migrations will be generated based on the MigrationConnection defined in the appsettings.Development.json file. This allows you to create and apply migrations to a specific tenant database during development without affecting the dynamic connection string logic used in production.
            // After the migrations are created, you can set isApplyMigration back to false to use the dynamic connection string for tenant databases in development as well. This approach allows you to manage migrations effectively while still supporting the multi-tenant architecture of your application.
            // Note: When isApplyMigration is true, the AuthDbContext will be registered with a fixed connection string (MigrationConnection) for the purpose of generating migrations. This means that any migrations created while this flag is true will be based on the schema of the database specified in MigrationConnection.
            // to replicate DB changes you call ApplyMigrations extension method on the WebApplication instance in Program.cs, this will apply any pending migrations to the database specified in MigrationConnection. This is useful during development to ensure that your tenant database schema is up to date with your latest migrations.
            // uncomment when creating new migrations, when use add-migration select auth.Api at the build-toolbar and at the package-console bar, the auth.data project is selected.
            // make sure the connection string in appsettings.Development.json is correct, then run add-migration command, after migration is created, comment it back to avoid accidentally running migrations on the tenant databases.
            if (settings != null && settings.DbConnections != null)
            {
                builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(settings.DbConnections.MigrationConnection,
                                   sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));
            }
        }
        else
        {
            //This is the standard multi‑tenant pattern for database-per-tenant.
            // DbContext with dynamic connection string based on the current tenant

            builder.Services.AddDbContext<AuthDbContext>((serviceProvider, options) =>
            {
                var tenantProvider = serviceProvider.GetRequiredService<ITenantManager>();
                var connectionString = tenantProvider.GetTenantDbConnection();

                options.UseSqlServer(connectionString, sql =>
                {
                    sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            });
        }
    }

    public static void RegisterValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IValidator<RoleComponentSystemAction>, RoleComponentSystemActionValidator>();
        builder.Services.AddScoped<IValidator<User>, UserValidator>();
        builder.Services.AddScoped<IValidator<App>, AppValidator>();
        builder.Services.AddScoped<IValidator<Role>, RoleValidator>();
        builder.Services.AddScoped<IValidator<RoleApp>, RoleAppValidator>();
        builder.Services.AddScoped<IValidator<RoleUser>, RoleUserValidator>();
        builder.Services.AddScoped<IValidator<UserRequest>, UserRequestValidator>();
        builder.Services.AddScoped<IValidator<UserRequestStatus>, UserRequestStatusValidator>();
        builder.Services.AddScoped<IValidator<ResourceRule>, ResourceRuleValidator>();
        //Template_Component_RegisterValidator
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IRoleComponentSystemActionRepository, RoleComponentSystemActionRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAppRepository, AppRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IRoleAppRepository, RoleAppRepository>();
        builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();
        builder.Services.AddScoped<IUserRequestRepository, UserRequestRepository>();
        builder.Services.AddScoped<IUserRequestStatusRepository, UserRequestStatusRepository>();
        builder.Services.AddScoped<IResourceRuleRepository, ResourceRuleRepository>();
        //Template_Component_RegisterRepository
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReportsService, ReportsService>();
        builder.Services.AddScoped<IOperationsService, OperationsService>();
        builder.Services.AddScoped<IRoleComponentSystemActionService, RoleComponentSystemActionService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAppService, AppService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IRoleAppService, RoleAppService>();
        builder.Services.AddScoped<IRoleUserService, RoleUserService>();
        builder.Services.AddScoped<IUserRequestService, UserRequestService>();
        builder.Services.AddScoped<IUserRequestStatusService, UserRequestStatusService>();
        builder.Services.AddScoped<IResourceRuleService, ResourceRuleService>();
        //Template_Component_RegisterService
    }

    public static void RegisterLists(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        // Get the current tenant's connection string from the TenantProvider and create a TenantSqlConfiguration that can be used by the lists to access the database.
        // This allows the lists to be tenant-aware and operate on the correct database for each request.
        // AddScoped is used here because the TenantProvider is scoped to the request, and we want to ensure that each list gets the correct connection string for the current tenant during that request. By registering TenantSqlConfiguration as scoped,
        //  we can safely inject it into the lists without worrying about cross-tenant data access issues.
        // ------------------------------------------------------------
        builder.Services.AddScoped(sp =>
        {
            var tenantProvider = sp.GetRequiredService<ITenantManager>();
            var connectionString = tenantProvider.GetTenantDbConnection();
            return new TenantSqlConfiguration(connectionString);
        });
        // ------------------------------------------------------------

        builder.Services.AddScoped<IRoleComponentSystemActionList>(sp =>
        {
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new RoleComponentSystemActionList(config.ConnectionString, null);
        });

        builder.Services.AddScoped<IUserList>(sp =>
        {
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new UserList(config.ConnectionString, null);
        });

        builder.Services.AddScoped<IAppList>(sp =>
        {
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new AppList(config.ConnectionString, null);
        });

        builder.Services.AddScoped<IRoleList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new RoleList(config.ConnectionString, resourceSecurity);
        });

        builder.Services.AddScoped<IRoleAppList>(sp =>
        {
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new RoleAppList(config.ConnectionString, null);
        });

        builder.Services.AddScoped<IRoleUserList>(sp =>
        {

            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new RoleUserList(config.ConnectionString, null);
        });

        builder.Services.AddScoped<IUserRequestList>(sp =>
        {
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new UserRequestList(config.ConnectionString, null);
        });

        builder.Services.AddScoped<IUserRequestStatusList>(sp =>
        {
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new UserRequestStatusList(config.ConnectionString, null);
        });

        builder.Services.AddScoped<IResourceRuleList>(sp =>
        {
            var config = sp.GetRequiredService<TenantSqlConfiguration>();   
            return new ResourceRuleList(config.ConnectionString, null);
        });
        //Template_Component_RegisterList
    }

    public static void RegisterReports(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        builder.Services.AddScoped(sp =>
        {
            var tenantProvider = sp.GetRequiredService<ITenantManager>();
            var connectionString = tenantProvider.GetTenantDbConnection();
            return new TenantSqlConfiguration(connectionString);
        });

        builder.Services.AddScoped<IRoleRepCompare>(sp =>
        {
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new RoleRepCompare(config.ConnectionString);
        });

        //Template_Component_RegisterReport
    }

    public static void RegisterClients(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        //Template_Component_RegisterHttpClient
    }
}
