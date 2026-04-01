using Bfs.Core.Auth;
using Bfs.Core.Config;
using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Core.Middleware;
using Bfs.Core.Services.Auth;
using Bfs.Core.TenantManagement;
using Bfs.Stores.Api.Validators;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data.Lists;
using Bfs.Stores.Data.Reports;
using Bfs.Stores.Data.Repositories;
using Bfs.Stores.Domain.Interfaces;
using Bfs.Stores.Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
        //if (settings != null && settings.DbConnections != null)
        //{
        //    builder.Services.AddDbContext<StoresDbContext>(options => options.UseSqlServer(settings.DbConnections.StoresConnection,
        //    sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null))
        //);
        //}

        //This is the standard multi‑tenant pattern for database-per-tenant.
        // DbContext with dynamic connection string based on the current tenant
        builder.Services.AddDbContext<StoresDbContext>((serviceProvider, options) =>
        {
            var tenantProvider = serviceProvider.GetRequiredService<ITenantProvider>();
            var connectionString = tenantProvider.GetCurrentTenantDbConnection();

            options.UseSqlServer(connectionString, sql =>
            {
                sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });
    }

    public static void RegisterValidators(this WebApplicationBuilder builder)
    {
            builder.Services.AddScoped<IValidator<StrStore>, StrStoreValidator>();
            builder.Services.AddScoped<IValidator<StrProduct>, StrProductValidator>();
            builder.Services.AddScoped<IValidator<StrTransaction>, StrTransactionValidator>();
            builder.Services.AddScoped<IValidator<StrEffectType>, StrEffectTypeValidator>();
            builder.Services.AddScoped<IValidator<StrThirdPartyType>, StrThirdPartyTypeValidator>();
            builder.Services.AddScoped<IValidator<StrUnit>, StrUnitValidator>();
            builder.Services.AddScoped<IValidator<StrCurrency>, StrCurrencyValidator>();
            builder.Services.AddScoped<IValidator<StrOperation>, StrOperationValidator>();
//Template_Component_RegisterValidator
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IStrStoreRepository, StrStoreRepository>();
            builder.Services.AddScoped<IStrProductRepository, StrProductRepository>();
            builder.Services.AddScoped<IStrTransactionRepository, StrTransactionRepository>();
            builder.Services.AddScoped<IStrEffectTypeRepository, StrEffectTypeRepository>();
            builder.Services.AddScoped<IStrThirdPartyTypeRepository, StrThirdPartyTypeRepository>();
            builder.Services.AddScoped<IStrUnitRepository, StrUnitRepository>();
            builder.Services.AddScoped<IStrCurrencyRepository, StrCurrencyRepository>();
            builder.Services.AddScoped<IStrOperationRepository, StrOperationRepository>();
//Template_Component_RegisterRepository
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReportsService, ReportsService>();
        builder.Services.AddScoped<IOperationsService,OperationsService>();
            builder.Services.AddScoped<IStrStoreService, StrStoreService>();
            builder.Services.AddScoped<IStrProductService, StrProductService>();
            builder.Services.AddScoped<IStrTransactionService, StrTransactionService>();
            builder.Services.AddScoped<IStrEffectTypeService, StrEffectTypeService>();
            builder.Services.AddScoped<IStrThirdPartyTypeService, StrThirdPartyTypeService>();
            builder.Services.AddScoped<IStrUnitService, StrUnitService>();
            builder.Services.AddScoped<IStrCurrencyService, StrCurrencyService>();
            builder.Services.AddScoped<IStrOperationService, StrOperationService>();
//Template_Component_RegisterService
    }

    public static void RegisterLists(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.StoresConnection;
            builder.Services.AddScoped<IStrStoreList>(provider =>
            {
                return new StrStoreList(dbConnection);
            });

            builder.Services.AddScoped<IStrProductList>(provider =>
            {
                return new StrProductList(dbConnection);
            });

            builder.Services.AddScoped<IStrTransactionList>(provider =>
            {
                return new StrTransactionList(dbConnection);
            });

            builder.Services.AddScoped<IStrEffectTypeList>(provider =>
            {
                return new StrEffectTypeList(dbConnection);
            });

            builder.Services.AddScoped<IStrThirdPartyTypeList>(provider =>
            {
                return new StrThirdPartyTypeList(dbConnection);
            });

            builder.Services.AddScoped<IStrUnitList>(provider =>
            {
                return new StrUnitList(dbConnection);
            });

            builder.Services.AddScoped<IStrCurrencyList>(provider =>
            {
                return new StrCurrencyList(dbConnection);
            });

            builder.Services.AddScoped<IStrOperationList>(provider =>
            {
                return new StrOperationList(dbConnection);
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
