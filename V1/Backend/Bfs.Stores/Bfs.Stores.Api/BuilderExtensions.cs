using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Bfs.Core.Auth;
using Bfs.Core.Config;
using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Core.Middleware;
using Bfs.Core.Services.Auth;
using Bfs.Core.TenantManagement;
using Bfs.Core.Services.Auth;
using Bfs.Stores.Api.Validators;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data.Lists;
using Bfs.Stores.Data.Reports;
using Bfs.Stores.Data.Repositories;
using Bfs.Stores.Domain.Interfaces;
using Bfs.Stores.Domain.Services;

namespace Bfs.Stores.Api;

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
        // if the system is the master system, we can register the MasterDbContext with a fixed connection string. This is because the master system is responsible for managing tenants and their connection strings, so it needs to have a stable connection to the master database. 

        //if (settings != null && settings.DbConnections != null)
        //{
        //    builder.Services.AddDbContext<Bfs.Master.Data.MasterDbContext>(options => options.UseSqlServer(settings.DbConnections.MasterConnection,
        //    sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null))
        //    );
        //}

        //This is the standard multi‑tenant pattern for database-per-tenant.
        // DbContext with dynamic connection string based on the current tenant

        builder.Services.AddDbContext<StoresDbContext>((serviceProvider, options) =>
        {
            var IsMigrationMode = false;
            if (IsMigrationMode)
            {
                options.UseSqlServer(settings.DbConnections.TestTenantConnection, sql =>
                {
                    sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            }
            else
            {
                var tenantProvider = serviceProvider.GetRequiredService<ITenantProvider>();
                var connectionString = tenantProvider.GetCurrentTenantDbConnection();

                options.UseSqlServer(connectionString, sql =>
                {
                    sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            }
        });
    }

    public static void RegisterValidators(this WebApplicationBuilder builder)
    {
            builder.Services.AddScoped<IValidator<Store>, StoreValidator>();
            builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
            builder.Services.AddScoped<IValidator<Transaction>, TransactionValidator>();
            builder.Services.AddScoped<IValidator<EffectType>, EffectTypeValidator>();
            builder.Services.AddScoped<IValidator<ThirdPartyType>, ThirdPartyTypeValidator>();
            builder.Services.AddScoped<IValidator<Unit>, UnitValidator>();
            builder.Services.AddScoped<IValidator<Currency>, CurrencyValidator>();
            builder.Services.AddScoped<IValidator<Operation>, OperationValidator>();
//Template_Component_RegisterValidator
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IEffectTypeRepository, EffectTypeRepository>();
            builder.Services.AddScoped<IThirdPartyTypeRepository, ThirdPartyTypeRepository>();
            builder.Services.AddScoped<IUnitRepository, UnitRepository>();
            builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            builder.Services.AddScoped<IOperationRepository, OperationRepository>();
//Template_Component_RegisterRepository
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReportsService, ReportsService>();
        builder.Services.AddScoped<IOperationsService,OperationsService>();
            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IEffectTypeService, EffectTypeService>();
            builder.Services.AddScoped<IThirdPartyTypeService, ThirdPartyTypeService>();
            builder.Services.AddScoped<IUnitService, UnitService>();
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();
            builder.Services.AddScoped<IOperationService, OperationService>();
//Template_Component_RegisterService
    }

    public static void RegisterLists(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.StoresConnection;
            builder.Services.AddScoped<IStoreList>(provider =>
            {
                return new StoreList(dbConnection);
            });

            builder.Services.AddScoped<IProductList>(provider =>
            {
                return new ProductList(dbConnection);
            });

            builder.Services.AddScoped<ITransactionList>(provider =>
            {
                return new TransactionList(dbConnection);
            });

            builder.Services.AddScoped<IEffectTypeList>(provider =>
            {
                return new EffectTypeList(dbConnection);
            });

            builder.Services.AddScoped<IThirdPartyTypeList>(provider =>
            {
                return new ThirdPartyTypeList(dbConnection);
            });

            builder.Services.AddScoped<IUnitList>(provider =>
            {
                return new UnitList(dbConnection);
            });

            builder.Services.AddScoped<ICurrencyList>(provider =>
            {
                return new CurrencyList(dbConnection);
            });

            builder.Services.AddScoped<IOperationList>(provider =>
            {
                return new OperationList(dbConnection);
            });

//Template_Component_RegisterList
        }
    }

    public static void RegisterReports(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.StoresConnection;
            builder.Services.AddScoped<IProductTransactionCompare>(provider =>
            {
                return new ProductTransactionCompare(dbConnection);
            });

//Template_Component_RegisterReport
        }
    }

    public static void RegisterClients(this WebApplicationBuilder builder, BfsSettings? settings)
    {
//Template_Component_RegisterHttpClient
    }
}
