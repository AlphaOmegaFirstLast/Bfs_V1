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
using Bfs.BestFit.Api.Validators;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data.Lists;
using Bfs.BestFit.Data.Reports;
using Bfs.BestFit.Data.Repositories;
using Bfs.BestFit.Domain.Interfaces;
using Bfs.BestFit.Domain.Services;

namespace Bfs.BestFit.Api;

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
            builder.Services.AddDbContext< BestFitDbContext >(options => options.UseSqlServer(settings.DbConnections.BestFitConnection,
            sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null))
        );
        }
    }

    public static void RegisterValidators(this WebApplicationBuilder builder)
    {
            builder.Services.AddScoped<IValidator<SystemTemplate>, SystemTemplateValidator>();
            builder.Services.AddScoped<IValidator<DataType>, DataTypeValidator>();
            builder.Services.AddScoped<IValidator<FilterType>, FilterTypeValidator>();
            builder.Services.AddScoped<IValidator<FormControlType>, FormControlTypeValidator>();
            builder.Services.AddScoped<IValidator<ComponentType>, ComponentTypeValidator>();
            builder.Services.AddScoped<IValidator<BackendDataType>, BackendDataTypeValidator>();
            builder.Services.AddScoped<IValidator<ActionType>, ActionTypeValidator>();
            builder.Services.AddScoped<IValidator<AggregateType>, AggregateTypeValidator>();
            builder.Services.AddScoped<IValidator<ChartElement>, ChartElementValidator>();
            builder.Services.AddScoped<IValidator<ActionLocation>, ActionLocationValidator>();
            builder.Services.AddScoped<IValidator<SystemAction>, SystemActionValidator>();
            builder.Services.AddScoped<IValidator<Component>, ComponentValidator>();
            builder.Services.AddScoped<IValidator<TableField>, TableFieldValidator>();
            builder.Services.AddScoped<IValidator<SystemInfo>, SystemInfoValidator>();
            builder.Services.AddScoped<IValidator<Client>, ClientValidator>();
            builder.Services.AddScoped<IValidator<CustomReports>, CustomReportsValidator>();
            builder.Services.AddScoped<IValidator<CustomFieldDefinition>, CustomFieldDefinitionValidator>();
            builder.Services.AddScoped<IValidator<BusinessAction>, BusinessActionValidator>();
            builder.Services.AddScoped<IValidator<ComponentSystemAction>, ComponentSystemActionValidator>();
            builder.Services.AddScoped<IValidator<ComponentBusinessAction>, ComponentBusinessActionValidator>();
            builder.Services.AddScoped<IValidator<DeploymentAzureStaging>, DeploymentAzureStagingValidator>();
            builder.Services.AddScoped<IValidator<DeploymentLocal>, DeploymentLocalValidator>();

//Template_Component_RegisterValidator
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISystemTemplateRepository, SystemTemplateRepository>();
            builder.Services.AddScoped<IDataTypeRepository, DataTypeRepository>();
            builder.Services.AddScoped<IFilterTypeRepository, FilterTypeRepository>();
            builder.Services.AddScoped<IFormControlTypeRepository, FormControlTypeRepository>();
            builder.Services.AddScoped<IComponentTypeRepository, ComponentTypeRepository>();
            builder.Services.AddScoped<IBackendDataTypeRepository, BackendDataTypeRepository>();
            builder.Services.AddScoped<IActionTypeRepository, ActionTypeRepository>();
            builder.Services.AddScoped<IAggregateTypeRepository, AggregateTypeRepository>();
            builder.Services.AddScoped<IChartElementRepository, ChartElementRepository>();
            builder.Services.AddScoped<IActionLocationRepository, ActionLocationRepository>();
            builder.Services.AddScoped<ISystemActionRepository, SystemActionRepository>();
            builder.Services.AddScoped<IComponentRepository, ComponentRepository>();
            builder.Services.AddScoped<ITableFieldRepository, TableFieldRepository>();
            builder.Services.AddScoped<ISystemInfoRepository, SystemInfoRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<ICustomReportsRepository, CustomReportsRepository>();
            builder.Services.AddScoped<ICustomFieldDefinitionRepository, CustomFieldDefinitionRepository>();
            builder.Services.AddScoped<IBusinessActionRepository, BusinessActionRepository>();
            builder.Services.AddScoped<IComponentSystemActionRepository, ComponentSystemActionRepository>();
            builder.Services.AddScoped<IComponentBusinessActionRepository, ComponentBusinessActionRepository>();
            builder.Services.AddScoped<IDeploymentAzureStagingRepository, DeploymentAzureStagingRepository>();
            builder.Services.AddScoped<IDeploymentLocalRepository, DeploymentLocalRepository>();

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
            builder.Services.AddScoped<IComponentTypeService, ComponentTypeService>();
            builder.Services.AddScoped<IBackendDataTypeService, BackendDataTypeService>();
            builder.Services.AddScoped<IActionTypeService, ActionTypeService>();
            builder.Services.AddScoped<IAggregateTypeService, AggregateTypeService>();
            builder.Services.AddScoped<IChartElementService, ChartElementService>();
            builder.Services.AddScoped<IActionLocationService, ActionLocationService>();
            builder.Services.AddScoped<ISystemActionService, SystemActionService>();
            builder.Services.AddScoped<IComponentService, ComponentService>();
            builder.Services.AddScoped<ITableFieldService, TableFieldService>();
            builder.Services.AddScoped<ISystemInfoService, SystemInfoService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<ICustomReportsService, CustomReportsService>();
            builder.Services.AddScoped<ICustomFieldDefinitionService, CustomFieldDefinitionService>();
            builder.Services.AddScoped<IBusinessActionService, BusinessActionService>();
            builder.Services.AddScoped<IComponentSystemActionService, ComponentSystemActionService>();
            builder.Services.AddScoped<IComponentBusinessActionService, ComponentBusinessActionService>();
            builder.Services.AddScoped<IDeploymentAzureStagingService, DeploymentAzureStagingService>();
            builder.Services.AddScoped<IDeploymentLocalService, DeploymentLocalService>();

//Template_Component_RegisterService
    }

    public static void RegisterLists(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.BestFitConnection;
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

            builder.Services.AddScoped<IComponentTypeList>(provider =>
            {
                return new ComponentTypeList(dbConnection);
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

            builder.Services.AddScoped<IChartElementList>(provider =>
            {
                return new ChartElementList(dbConnection);
            });

            builder.Services.AddScoped<IActionLocationList>(provider =>
            {
                return new ActionLocationList(dbConnection);
            });

            builder.Services.AddScoped<ISystemActionList>(provider =>
            {
                return new SystemActionList(dbConnection);
            });

            builder.Services.AddScoped<IComponentList>(provider =>
            {
                return new ComponentList(dbConnection);
            });

            builder.Services.AddScoped<ITableFieldList>(provider =>
            {
                return new TableFieldList(dbConnection);
            });

            builder.Services.AddScoped<ISystemInfoList>(provider =>
            {
                return new SystemInfoList(dbConnection);
            });

            builder.Services.AddScoped<IClientList>(provider =>
            {
                return new ClientList(dbConnection);
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

            builder.Services.AddScoped<IComponentSystemActionList>(provider =>
            {
                return new ComponentSystemActionList(dbConnection);
            });

            builder.Services.AddScoped<IComponentBusinessActionList>(provider =>
            {
                return new ComponentBusinessActionList(dbConnection);
            });

            builder.Services.AddScoped<IDeploymentAzureStagingList>(provider =>
            {
                return new DeploymentAzureStagingList(dbConnection);
            });

            builder.Services.AddScoped<IDeploymentLocalList>(provider =>
            {
                return new DeploymentLocalList(dbConnection);
            });

//Template_Component_RegisterList
        }
    }

    public static void RegisterReports(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        if (settings != null && settings.DbConnections != null)
        {
            var dbConnection = settings.DbConnections.BestFitConnection;
            builder.Services.AddScoped<IStructureReportReport>(provider =>
            {
                return new StructureReportReport(dbConnection);
            });

            builder.Services.AddScoped<IDataType1Report>(provider =>
            {
                return new DataType1Report(dbConnection);
            });

//Template_Component_RegisterReport
        }
    }

    public static void RegisterClients(this WebApplicationBuilder builder, BfsSettings? settings)
    {
//Template_Component_RegisterHttpClient
    }
}
