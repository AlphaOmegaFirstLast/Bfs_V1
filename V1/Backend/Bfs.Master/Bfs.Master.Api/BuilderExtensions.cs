using Bfs.Core.Auth;
using Bfs.Core.Config;
using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Core.Middleware;
using Bfs.Core.Services.Auth;
using Bfs.Core.Services.Auth;
using Bfs.Core.Services.Security;
using Bfs.Core.TenantManagement;
using Bfs.Master.Api.Validators;
using Bfs.Master.Contracts;
using Bfs.Master.Data;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data.Lists;
using Bfs.Master.Data.Reports;
using Bfs.Master.Data.Repositories;
using Bfs.Master.Domain.Interfaces;
using Bfs.Master.Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Bfs.Master.Api;

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
            // Note: When isApplyMigration is true, the StoresDbContext will be registered with a fixed connection string (MigrationConnection) for the purpose of generating migrations. This means that any migrations created while this flag is true will be based on the schema of the database specified in MigrationConnection.
            // to replicate DB changes you call ApplyMigrations extension method on the WebApplication instance in Program.cs, this will apply any pending migrations to the database specified in MigrationConnection. This is useful during development to ensure that your tenant database schema is up to date with your latest migrations.
            // uncomment when creating new migrations, when use add-migration select Stores.Api at the build-toolbar and at the package-console bar, the auth.data project is selected.
            // make sure the connection string in appsettings.Development.json is correct, then run add-migration command, after migration is created, comment it back to avoid accidentally running migrations on the tenant databases.
            if (settings != null && settings.DbConnections != null)
            {
                builder.Services.AddDbContext<MasterDbContext>(options => options.UseSqlServer(settings.DbConnections.MigrationConnection,
                                   sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));
            }
        }
        else
        {
            //This is the standard multi‑tenant pattern for database-per-tenant.
            // DbContext with dynamic connection string based on the current tenant
            // if settings.IsMasterSystem is true, then the connection string will be MasterConnection.
            builder.Services.AddDbContext<MasterDbContext>((serviceProvider, options) =>
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
            builder.Services.AddScoped<IValidator<SystemTemplate>, SystemTemplateValidator>();
            builder.Services.AddScoped<IValidator<DataType>, DataTypeValidator>();
            builder.Services.AddScoped<IValidator<FilterType>, FilterTypeValidator>();
            builder.Services.AddScoped<IValidator<FormControlType>, FormControlTypeValidator>();
            builder.Services.AddScoped<IValidator<BackendDataType>, BackendDataTypeValidator>();
            builder.Services.AddScoped<IValidator<ActionType>, ActionTypeValidator>();
            builder.Services.AddScoped<IValidator<AggregateType>, AggregateTypeValidator>();
            builder.Services.AddScoped<IValidator<ActionLocation>, ActionLocationValidator>();
            builder.Services.AddScoped<IValidator<ChartElement>, ChartElementValidator>();
            builder.Services.AddScoped<IValidator<WriterType>, WriterTypeValidator>();
            builder.Services.AddScoped<IValidator<BfsComponent>, BfsComponentValidator>();
            builder.Services.AddScoped<IValidator<BfsField>, BfsFieldValidator>();
            builder.Services.AddScoped<IValidator<BfsSystem>, BfsSystemValidator>();
            builder.Services.AddScoped<IValidator<BfsTenant>, BfsTenantValidator>();
            builder.Services.AddScoped<IValidator<BfsTenantSystem>, BfsTenantSystemValidator>();
            builder.Services.AddScoped<IValidator<BfsComponentBusinessAction>, BfsComponentBusinessActionValidator>();
            builder.Services.AddScoped<IValidator<CustomReports>, CustomReportsValidator>();
            builder.Services.AddScoped<IValidator<CustomFieldDefinition>, CustomFieldDefinitionValidator>();
            builder.Services.AddScoped<IValidator<SystemAction>, SystemActionValidator>();
            builder.Services.AddScoped<IValidator<BusinessAction>, BusinessActionValidator>();
            builder.Services.AddScoped<IValidator<DeploymentAzure>, DeploymentAzureValidator>();
            builder.Services.AddScoped<IValidator<DeploymentLocal>, DeploymentLocalValidator>();
            builder.Services.AddScoped<IValidator<BfsComponentSystemAction>, BfsComponentSystemActionValidator>();
//Template_Component_RegisterValidator
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISystemTemplateRepository, SystemTemplateRepository>();
            builder.Services.AddScoped<IDataTypeRepository, DataTypeRepository>();
            builder.Services.AddScoped<IFilterTypeRepository, FilterTypeRepository>();
            builder.Services.AddScoped<IFormControlTypeRepository, FormControlTypeRepository>();
            builder.Services.AddScoped<IBackendDataTypeRepository, BackendDataTypeRepository>();
            builder.Services.AddScoped<IActionTypeRepository, ActionTypeRepository>();
            builder.Services.AddScoped<IAggregateTypeRepository, AggregateTypeRepository>();
            builder.Services.AddScoped<IActionLocationRepository, ActionLocationRepository>();
            builder.Services.AddScoped<IChartElementRepository, ChartElementRepository>();
            builder.Services.AddScoped<IWriterTypeRepository, WriterTypeRepository>();
            builder.Services.AddScoped<IBfsComponentRepository, BfsComponentRepository>();
            builder.Services.AddScoped<IBfsFieldRepository, BfsFieldRepository>();
            builder.Services.AddScoped<IBfsSystemRepository, BfsSystemRepository>();
            builder.Services.AddScoped<IBfsTenantRepository, BfsTenantRepository>();
            builder.Services.AddScoped<IBfsTenantSystemRepository, BfsTenantSystemRepository>();
            builder.Services.AddScoped<IBfsComponentBusinessActionRepository, BfsComponentBusinessActionRepository>();
            builder.Services.AddScoped<ICustomReportsRepository, CustomReportsRepository>();
            builder.Services.AddScoped<ICustomFieldDefinitionRepository, CustomFieldDefinitionRepository>();
            builder.Services.AddScoped<ISystemActionRepository, SystemActionRepository>();
            builder.Services.AddScoped<IBusinessActionRepository, BusinessActionRepository>();
            builder.Services.AddScoped<IDeploymentAzureRepository, DeploymentAzureRepository>();
            builder.Services.AddScoped<IDeploymentLocalRepository, DeploymentLocalRepository>();
            builder.Services.AddScoped<IBfsComponentSystemActionRepository, BfsComponentSystemActionRepository>();
//Template_Component_RegisterRepository
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReportsService, ReportsService>();
        builder.Services.AddScoped<IOperationsService,OperationsService>();
            builder.Services.AddScoped<ISystemTemplateService, SystemTemplateService>();
            builder.Services.AddScoped<IDataTypeService, DataTypeService>();
            builder.Services.AddScoped<IFilterTypeService, FilterTypeService>();
            builder.Services.AddScoped<IFormControlTypeService, FormControlTypeService>();
            builder.Services.AddScoped<IBackendDataTypeService, BackendDataTypeService>();
            builder.Services.AddScoped<IActionTypeService, ActionTypeService>();
            builder.Services.AddScoped<IAggregateTypeService, AggregateTypeService>();
            builder.Services.AddScoped<IActionLocationService, ActionLocationService>();
            builder.Services.AddScoped<IChartElementService, ChartElementService>();
            builder.Services.AddScoped<IWriterTypeService, WriterTypeService>();
            builder.Services.AddScoped<IBfsComponentService, BfsComponentService>();
            builder.Services.AddScoped<IBfsFieldService, BfsFieldService>();
            builder.Services.AddScoped<IBfsSystemService, BfsSystemService>();
            builder.Services.AddScoped<IBfsTenantService, BfsTenantService>();
            builder.Services.AddScoped<IBfsTenantSystemService, BfsTenantSystemService>();
            builder.Services.AddScoped<IBfsComponentBusinessActionService, BfsComponentBusinessActionService>();
            builder.Services.AddScoped<ICustomReportsService, CustomReportsService>();
            builder.Services.AddScoped<ICustomFieldDefinitionService, CustomFieldDefinitionService>();
            builder.Services.AddScoped<ISystemActionService, SystemActionService>();
            builder.Services.AddScoped<IBusinessActionService, BusinessActionService>();
            builder.Services.AddScoped<IDeploymentAzureService, DeploymentAzureService>();
            builder.Services.AddScoped<IDeploymentLocalService, DeploymentLocalService>();
            builder.Services.AddScoped<IBfsComponentSystemActionService, BfsComponentSystemActionService>();
//Template_Component_RegisterService
    }

    public static void RegisterLists(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.MasterConnection;
            builder.Services.AddScoped<ISystemTemplateList>(provider =>
            {
                return new SystemTemplateList(dbConnection);
            });

            builder.Services.AddScoped<IDataTypeList>(provider =>
            {
                return new DataTypeList(dbConnection);
            });

            builder.Services.AddScoped<IFilterTypeList>(provider =>
            {
                return new FilterTypeList(dbConnection);
            });

            builder.Services.AddScoped<IFormControlTypeList>(provider =>
            {
                return new FormControlTypeList(dbConnection);
            });

            builder.Services.AddScoped<IBackendDataTypeList>(provider =>
            {
                return new BackendDataTypeList(dbConnection);
            });

            builder.Services.AddScoped<IActionTypeList>(provider =>
            {
                return new ActionTypeList(dbConnection);
            });

            builder.Services.AddScoped<IAggregateTypeList>(provider =>
            {
                return new AggregateTypeList(dbConnection);
            });

            builder.Services.AddScoped<IActionLocationList>(provider =>
            {
                return new ActionLocationList(dbConnection);
            });

            builder.Services.AddScoped<IChartElementList>(provider =>
            {
                return new ChartElementList(dbConnection);
            });

            builder.Services.AddScoped<IWriterTypeList>(provider =>
            {
                return new WriterTypeList(dbConnection);
            });

            builder.Services.AddScoped<IBfsSystemList>(provider =>
            {
                return new BfsSystemList(dbConnection);
            });

            builder.Services.AddScoped<IBfsTenantList>(provider =>
            {
                return new BfsTenantList(dbConnection);
            });

            builder.Services.AddScoped<IBfsTenantSystemList>(provider =>
            {
                return new BfsTenantSystemList(dbConnection);
            });

            builder.Services.AddScoped<IBfsComponentBusinessActionList>(provider =>
            {
                return new BfsComponentBusinessActionList(dbConnection);
            });

            builder.Services.AddScoped<ICustomReportsList>(provider =>
            {
                return new CustomReportsList(dbConnection);
            });

            builder.Services.AddScoped<ICustomFieldDefinitionList>(provider =>
            {
                return new CustomFieldDefinitionList(dbConnection);
            });

            builder.Services.AddScoped<ISystemActionList>(provider =>
            {
                return new SystemActionList(dbConnection);
            });

            builder.Services.AddScoped<IBusinessActionList>(provider =>
            {
                return new BusinessActionList(dbConnection);
            });

            builder.Services.AddScoped<IDeploymentAzureList>(provider =>
            {
                return new DeploymentAzureList(dbConnection);
            });

            builder.Services.AddScoped<IDeploymentLocalList>(provider =>
            {
                return new DeploymentLocalList(dbConnection);
            });

            builder.Services.AddScoped<IBfsComponentSystemActionList>(provider =>
            {
                return new BfsComponentSystemActionList(dbConnection);
            });

        builder.Services.AddScoped<IBfsComponentList>(sp =>
        {
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new BfsComponentList(config.ConnectionString,null);
        });
            builder.Services.AddScoped<IBfsFieldList>(sp =>
            {
                var config = sp.GetRequiredService<TenantSqlConfiguration>();
                return new BfsFieldList(config.ConnectionString, null);
            });
            //Template_Component_RegisterList
        }
    }

    public static void RegisterReports(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.MasterConnection;
            builder.Services.AddScoped<IStructureCompare>(provider =>
            {
                return new StructureCompare(dbConnection);
            });

//Template_Component_RegisterReport
        }
    }

    public static void RegisterClients(this WebApplicationBuilder builder, BfsSettings? settings)
    {
//Template_Component_RegisterHttpClient
    }
}
