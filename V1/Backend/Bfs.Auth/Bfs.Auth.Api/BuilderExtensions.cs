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
using Bfs.Auth.Api.Validators;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data.Lists;
using Bfs.Auth.Data.Reports;
using Bfs.Auth.Data.Repositories;
using Bfs.Auth.Domain.Interfaces;
using Bfs.Auth.Domain.Services;

namespace Bfs.Auth.Api;

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
            builder.Services.AddDbContext< AuthDbContext >(options => options.UseSqlServer(settings.DbConnections.AuthConnection,
            sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null))
        );
        }
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
                return new AuthRoleComponentSystemActionList(dbConnection);
            });

            builder.Services.AddScoped<IAuthUserList>(provider =>
            {
                return new AuthUserList(dbConnection);
            });

            builder.Services.AddScoped<IAuthAppList>(provider =>
            {
                return new AuthAppList(dbConnection);
            });

            builder.Services.AddScoped<IAuthRoleList>(provider =>
            {
                return new AuthRoleList(dbConnection);
            });

            builder.Services.AddScoped<IAuthRoleAppList>(provider =>
            {
                return new AuthRoleAppList(dbConnection);
            });

            builder.Services.AddScoped<IAuthRoleUserList>(provider =>
            {
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
