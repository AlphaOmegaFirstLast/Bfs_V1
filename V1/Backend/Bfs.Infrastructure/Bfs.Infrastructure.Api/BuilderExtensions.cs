using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Bfs.Core.Config;
using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Core.Middleware;
using Bfs.Core.Services.Auth;
using Bfs.Infrastructure.Api.Validators;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data.Lists;
using Bfs.Infrastructure.Data.Reports;
using Bfs.Infrastructure.Data.Repositories;
using Bfs.Infrastructure.Domain.Interfaces;
using Bfs.Infrastructure.Domain.Services;

namespace Bfs.Infrastructure.Api;

public static class BuilderExtensions
{
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

        // Register Authorization handlers and policy provider. that can handle dynamic policies.
        builder.Services.AddSingleton<IAuthorizationHandler, MultiClaimRequirementHandler>();
        builder.Services.AddSingleton<IAuthorizationPolicyProvider, DynamicAuthorizationPolicyProvider>();
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

    public static void RegisterScopeData(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IScopeData, ScopeData>();
    }

    public static void RegisterDbContext(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            builder.Services.AddDbContext< InfrastructureDbContext >(options => options.UseSqlServer(settings.DbConnections.InfrastructureConnection,
            sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null))
        );
        }
    }

    public static void RegisterValidators(this WebApplicationBuilder builder)
    {
            builder.Services.AddScoped<IValidator<DataType>, DataTypeValidator>();
            builder.Services.AddScoped<IValidator<BfsComponent>, BfsComponentValidator>();
            builder.Services.AddScoped<IValidator<BfsField>, BfsFieldValidator>();
            builder.Services.AddScoped<IValidator<BfsSystem>, BfsSystemValidator>();
            builder.Services.AddScoped<IValidator<BfsClient>, BfsClientValidator>();
            builder.Services.AddScoped<IValidator<CustomReports>, CustomReportsValidator>();
            builder.Services.AddScoped<IValidator<CustomFieldDefinition>, CustomFieldDefinitionValidator>();
            builder.Services.AddScoped<IValidator<BfsComponentSystemAction>, BfsComponentSystemActionValidator>();
            builder.Services.AddScoped<IValidator<BfsComponentBusinessAction>, BfsComponentBusinessActionValidator>();

            builder.Services.AddScoped<IValidator<DeploymentLocal>, DeploymentLocalValidator>();
            builder.Services.AddScoped<IValidator<SystemTemplate>, SystemTemplateValidator>();
            builder.Services.AddScoped<IValidator<FilterType>, FilterTypeValidator>();
            builder.Services.AddScoped<IValidator<FormControlType>, FormControlTypeValidator>();
            builder.Services.AddScoped<IValidator<BackendDataType>, BackendDataTypeValidator>();
            builder.Services.AddScoped<IValidator<ActionType>, ActionTypeValidator>();
            builder.Services.AddScoped<IValidator<AggregateType>, AggregateTypeValidator>();
            builder.Services.AddScoped<IValidator<ActionLocation>, ActionLocationValidator>();
            builder.Services.AddScoped<IValidator<ChartElement>, ChartElementValidator>();
            builder.Services.AddScoped<IValidator<DeploymentAzure>, DeploymentAzureValidator>();

        builder.Services.AddScoped<IValidator<BusinessAction>, BusinessActionValidator>();
        builder.Services.AddScoped<IValidator<SystemAction>, SystemActionValidator>();
            builder.Services.AddScoped<IValidator<WriterType>, WriterTypeValidator>();
            builder.Services.AddScoped<IValidator<BfsClientSystem>, BfsClientSystemValidator>();
            builder.Services.AddScoped<IValidator<BfsTenant>, BfsTenantValidator>();
            builder.Services.AddScoped<IValidator<BfsTenantSystem>, BfsTenantSystemValidator>();
//Template_Component_RegisterValidator
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDataTypeRepository, DataTypeRepository>();
            builder.Services.AddScoped<IBfsComponentRepository, BfsComponentRepository>();
            builder.Services.AddScoped<IBfsFieldRepository, BfsFieldRepository>();
            builder.Services.AddScoped<IBfsSystemRepository, BfsSystemRepository>();
            builder.Services.AddScoped<IBfsClientRepository, BfsClientRepository>();
            builder.Services.AddScoped<ICustomReportsRepository, CustomReportsRepository>();
            builder.Services.AddScoped<ICustomFieldDefinitionRepository, CustomFieldDefinitionRepository>();
            builder.Services.AddScoped<IBfsComponentSystemActionRepository, BfsComponentSystemActionRepository>();
            builder.Services.AddScoped<IBfsComponentBusinessActionRepository, BfsComponentBusinessActionRepository>();

            builder.Services.AddScoped<IDeploymentLocalRepository, DeploymentLocalRepository>();
            builder.Services.AddScoped<ISystemTemplateRepository, SystemTemplateRepository>();
            builder.Services.AddScoped<IFilterTypeRepository, FilterTypeRepository>();
            builder.Services.AddScoped<IFormControlTypeRepository, FormControlTypeRepository>();
            builder.Services.AddScoped<IBackendDataTypeRepository, BackendDataTypeRepository>();
            builder.Services.AddScoped<IActionTypeRepository, ActionTypeRepository>();
            builder.Services.AddScoped<IAggregateTypeRepository, AggregateTypeRepository>();
            builder.Services.AddScoped<IActionLocationRepository, ActionLocationRepository>();
            builder.Services.AddScoped<IChartElementRepository, ChartElementRepository>();
            builder.Services.AddScoped<IDeploymentAzureRepository, DeploymentAzureRepository>();

        builder.Services.AddScoped<IBusinessActionRepository, BusinessActionRepository>();
        builder.Services.AddScoped<ISystemActionRepository, SystemActionRepository>();
            builder.Services.AddScoped<IWriterTypeRepository, WriterTypeRepository>();
            builder.Services.AddScoped<IBfsClientSystemRepository, BfsClientSystemRepository>();
            builder.Services.AddScoped<IBfsTenantRepository, BfsTenantRepository>();
            builder.Services.AddScoped<IBfsTenantSystemRepository, BfsTenantSystemRepository>();
//Template_Component_RegisterRepository
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReportsService, ReportsService>();
        builder.Services.AddScoped<IOperationsService,OperationsService>();
            builder.Services.AddScoped<IDataTypeService, DataTypeService>();
            builder.Services.AddScoped<IBfsComponentService, BfsComponentService>();
            builder.Services.AddScoped<IBfsFieldService, BfsFieldService>();
            builder.Services.AddScoped<IBfsSystemService, BfsSystemService>();
            builder.Services.AddScoped<IBfsClientService, BfsClientService>();
            builder.Services.AddScoped<ICustomReportsService, CustomReportsService>();
            builder.Services.AddScoped<ICustomFieldDefinitionService, CustomFieldDefinitionService>();
            builder.Services.AddScoped<IBfsComponentSystemActionService, BfsComponentSystemActionService>();
            builder.Services.AddScoped<IBfsComponentBusinessActionService, BfsComponentBusinessActionService>();

            builder.Services.AddScoped<IDeploymentLocalService, DeploymentLocalService>();
            builder.Services.AddScoped<ISystemTemplateService, SystemTemplateService>();
            builder.Services.AddScoped<IFilterTypeService, FilterTypeService>();
            builder.Services.AddScoped<IFormControlTypeService, FormControlTypeService>();
            builder.Services.AddScoped<IBackendDataTypeService, BackendDataTypeService>();
            builder.Services.AddScoped<IActionTypeService, ActionTypeService>();
            builder.Services.AddScoped<IAggregateTypeService, AggregateTypeService>();
            builder.Services.AddScoped<IActionLocationService, ActionLocationService>();
            builder.Services.AddScoped<IChartElementService, ChartElementService>();
            builder.Services.AddScoped<IDeploymentAzureService, DeploymentAzureService>();
        builder.Services.AddScoped<IBusinessActionService, BusinessActionService>();
        builder.Services.AddScoped<ISystemActionService, SystemActionService>();
            builder.Services.AddScoped<IWriterTypeService, WriterTypeService>();
            builder.Services.AddScoped<IBfsClientSystemService, BfsClientSystemService>();
            builder.Services.AddScoped<IBfsTenantService, BfsTenantService>();
            builder.Services.AddScoped<IBfsTenantSystemService, BfsTenantSystemService>();
//Template_Component_RegisterService
    }

    public static void RegisterLists(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.InfrastructureConnection;
            builder.Services.AddScoped<IDataTypeList>(provider =>
            {
                return new DataTypeList(dbConnection);
            });

            builder.Services.AddScoped<IBfsComponentList>(provider =>
            {
                return new BfsComponentList(dbConnection);
            });

            builder.Services.AddScoped<IBfsFieldList>(provider =>
            {
                return new BfsFieldList(dbConnection);
            });

            builder.Services.AddScoped<IBfsSystemList>(provider =>
            {
                return new BfsSystemList(dbConnection);
            });

            builder.Services.AddScoped<IBfsClientList>(provider =>
            {
                return new BfsClientList(dbConnection);
            });

            builder.Services.AddScoped<ICustomReportsList>(provider =>
            {
                return new CustomReportsList(dbConnection);
            });

            builder.Services.AddScoped<ICustomFieldDefinitionList>(provider =>
            {
                return new CustomFieldDefinitionList(dbConnection);
            });

            builder.Services.AddScoped<IBusinessActionList>(provider =>
            {
                return new BusinessActionList(dbConnection);
            });

            builder.Services.AddScoped<IBfsComponentSystemActionList>(provider =>
            {
                return new BfsComponentSystemActionList(dbConnection);
            });

            builder.Services.AddScoped<IBfsComponentBusinessActionList>(provider =>
            {
                return new BfsComponentBusinessActionList(dbConnection);
            });

            builder.Services.AddScoped<IDeploymentLocalList>(provider =>
            {
                return new DeploymentLocalList(dbConnection);
            });

            builder.Services.AddScoped<ISystemTemplateList>(provider =>
            {
                return new SystemTemplateList(dbConnection);
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

            builder.Services.AddScoped<IDeploymentAzureList>(provider =>
            {
                return new DeploymentAzureList(dbConnection);
            });

            builder.Services.AddScoped<ISystemActionList>(provider =>
            {
                return new SystemActionList(dbConnection);
            });

            builder.Services.AddScoped<IWriterTypeList>(provider =>
            {
                return new WriterTypeList(dbConnection);
            });

            builder.Services.AddScoped<IBfsClientSystemList>(provider =>
            {
                return new BfsClientSystemList(dbConnection);
            });

            builder.Services.AddScoped<IBfsTenantList>(provider =>
            {
                return new BfsTenantList(dbConnection);
            });

            builder.Services.AddScoped<IBfsTenantSystemList>(provider =>
            {
                return new BfsTenantSystemList(dbConnection);
            });

//Template_Component_RegisterList
        }
    }

    public static void RegisterReports(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.InfrastructureConnection;

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
