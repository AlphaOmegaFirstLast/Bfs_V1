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
using Bfs.Core.Services.Security;
using Bfs.Core.TenantManagement;
using Bfs.StockEx.Api.Validators;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data.Lists;
using Bfs.StockEx.Data.Reports;
using Bfs.StockEx.Data.Repositories;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.StockEx.Domain.Services;

namespace Bfs.StockEx.Api;

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
                builder.Services.AddDbContext<StockExDbContext>(options => options.UseSqlServer(settings.DbConnections.MigrationConnection,
                                   sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));
            }
        }
        else
        {
            //This is the standard multi‑tenant pattern for database-per-tenant.
            // DbContext with dynamic connection string based on the current tenant

            builder.Services.AddDbContext<StockExDbContext>((serviceProvider, options) =>
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
            builder.Services.AddScoped<IValidator<TradingRoom>, TradingRoomValidator>();
            builder.Services.AddScoped<IValidator<Broker>, BrokerValidator>();
            builder.Services.AddScoped<IValidator<Investor>, InvestorValidator>();
            builder.Services.AddScoped<IValidator<BrokerAgreement>, BrokerAgreementValidator>();
            builder.Services.AddScoped<IValidator<CashTransaction>, CashTransactionValidator>();
            builder.Services.AddScoped<IValidator<Coupon>, CouponValidator>();
            builder.Services.AddScoped<IValidator<Currency>, CurrencyValidator>();
            builder.Services.AddScoped<IValidator<CurrentPrice>, CurrentPriceValidator>();
            builder.Services.AddScoped<IValidator<ExpensesType>, ExpensesTypeValidator>();
            builder.Services.AddScoped<IValidator<InvestorBrokerFund>, InvestorBrokerFundValidator>();
            builder.Services.AddScoped<IValidator<OverdraftPortfolio>, OverdraftPortfolioValidator>();
            builder.Services.AddScoped<IValidator<SsPortfolio>, SsPortfolioValidator>();
            builder.Services.AddScoped<IValidator<SsPortfolioBalance>, SsPortfolioBalanceValidator>();
            builder.Services.AddScoped<IValidator<SspStock>, SspStockValidator>();
            builder.Services.AddScoped<IValidator<SspTransaction>, SspTransactionValidator>();
            builder.Services.AddScoped<IValidator<StockShare>, StockShareValidator>();
            builder.Services.AddScoped<IValidator<EffectType>, EffectTypeValidator>();
            builder.Services.AddScoped<IValidator<TransferCostType>, TransferCostTypeValidator>();
            builder.Services.AddScoped<IValidator<CouponStatus>, CouponStatusValidator>();
            builder.Services.AddScoped<IValidator<TransactionType>, TransactionTypeValidator>();
            builder.Services.AddScoped<IValidator<CalculationMethod>, CalculationMethodValidator>();
            builder.Services.AddScoped<IValidator<SourceType>, SourceTypeValidator>();
            builder.Services.AddScoped<IValidator<StockFieldType>, StockFieldTypeValidator>();
            builder.Services.AddScoped<IValidator<CouponType>, CouponTypeValidator>();
            builder.Services.AddScoped<IValidator<CustomReports>, CustomReportsValidator>();
            builder.Services.AddScoped<IValidator<StockEntityType>, StockEntityTypeValidator>();
//Template_Component_RegisterValidator
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ITradingRoomRepository, TradingRoomRepository>();
            builder.Services.AddScoped<IBrokerRepository, BrokerRepository>();
            builder.Services.AddScoped<IInvestorRepository, InvestorRepository>();
            builder.Services.AddScoped<IBrokerAgreementRepository, BrokerAgreementRepository>();
            builder.Services.AddScoped<ICashTransactionRepository, CashTransactionRepository>();
            builder.Services.AddScoped<ICouponRepository, CouponRepository>();
            builder.Services.AddScoped<ICouponTypeRepository, CouponTypeRepository>();
            builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            builder.Services.AddScoped<ICurrentPriceRepository, CurrentPriceRepository>();
            builder.Services.AddScoped<IExpensesTypeRepository, ExpensesTypeRepository>();
            builder.Services.AddScoped<IInvestorBrokerFundRepository, InvestorBrokerFundRepository>();
            builder.Services.AddScoped<IOverdraftPortfolioRepository, OverdraftPortfolioRepository>();
            builder.Services.AddScoped<ISsPortfolioRepository, SsPortfolioRepository>();
            builder.Services.AddScoped<ISsPortfolioBalanceRepository, SsPortfolioBalanceRepository>();
            builder.Services.AddScoped<ISspStockRepository, SspStockRepository>();
            builder.Services.AddScoped<ISspTransactionRepository, SspTransactionRepository>();
            builder.Services.AddScoped<IStockShareRepository, StockShareRepository>();

            builder.Services.AddScoped<ITransferCostTypeRepository, TransferCostTypeRepository>();
            builder.Services.AddScoped<ICouponStatusRepository, CouponStatusRepository>();
            builder.Services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
            builder.Services.AddScoped<IEffectTypeRepository, EffectTypeRepository>();
            builder.Services.AddScoped<IStockEntityTypeRepository, StockEntityTypeRepository>();
            builder.Services.AddScoped<ICalculationMethodRepository, CalculationMethodRepository>();
            builder.Services.AddScoped<ISourceTypeRepository, SourceTypeRepository>();
            builder.Services.AddScoped<IStockFieldTypeRepository, StockFieldTypeRepository>();
            builder.Services.AddScoped<ICustomReportsRepository, CustomReportsRepository>();
//Template_Component_RegisterRepository
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IReportsService, ReportsService>();
        builder.Services.AddScoped<IOperationsService,OperationsService>();
            builder.Services.AddScoped<ITradingRoomService, TradingRoomService>();
            builder.Services.AddScoped<IBrokerService, BrokerService>();
            builder.Services.AddScoped<IInvestorService, InvestorService>();
            builder.Services.AddScoped<IBrokerAgreementService, BrokerAgreementService>();
            builder.Services.AddScoped<ICashTransactionService, CashTransactionService>();
            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<ICouponTypeService, CouponTypeService>();
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();
            builder.Services.AddScoped<ICurrentPriceService, CurrentPriceService>();
            builder.Services.AddScoped<IExpensesTypeService, ExpensesTypeService>();
            builder.Services.AddScoped<IInvestorBrokerFundService, InvestorBrokerFundService>();
            builder.Services.AddScoped<IOverdraftPortfolioService, OverdraftPortfolioService>();
            builder.Services.AddScoped<ISsPortfolioService, SsPortfolioService>();
            builder.Services.AddScoped<ISsPortfolioBalanceService, SsPortfolioBalanceService>();
            builder.Services.AddScoped<ISspStockService, SspStockService>();
            builder.Services.AddScoped<ISspTransactionService, SspTransactionService>();
            builder.Services.AddScoped<IStockShareService, StockShareService>();

            builder.Services.AddScoped<ITransferCostTypeService, TransferCostTypeService>();
            builder.Services.AddScoped<ICouponStatusService, CouponStatusService>();
            builder.Services.AddScoped<ITransactionTypeService, TransactionTypeService>();
            builder.Services.AddScoped<IEffectTypeService, EffectTypeService>();
            builder.Services.AddScoped<IStockEntityTypeService, StockEntityTypeService>();
            builder.Services.AddScoped<ICalculationMethodService, CalculationMethodService>();
            builder.Services.AddScoped<ISourceTypeService, SourceTypeService>();
            builder.Services.AddScoped<IStockFieldTypeService, StockFieldTypeService>();
            builder.Services.AddScoped<ICustomReportsService, CustomReportsService>();
//Template_Component_RegisterService
    }

    public static void RegisterLists(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        builder.Services.AddScoped<ITradingRoomList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new TradingRoomList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IBrokerList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new BrokerList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IInvestorList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new InvestorList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IBrokerAgreementList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new BrokerAgreementList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ICashTransactionList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new CashTransactionList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ICouponList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new CouponList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ICouponTypeList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new CouponTypeList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ICurrencyList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new CurrencyList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ICurrentPriceList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new CurrentPriceList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IExpensesTypeList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new ExpensesTypeList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IInvestorBrokerFundList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new InvestorBrokerFundList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IOverdraftPortfolioList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new OverdraftPortfolioList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ISsPortfolioList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new SsPortfolioList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ISsPortfolioBalanceList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new SsPortfolioBalanceList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ISspStockList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new SspStockList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ISspTransactionList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new SspTransactionList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IStockShareList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new StockShareList(config.ConnectionString, resourceSecurity);
        });

        builder.Services.AddScoped<ITransferCostTypeList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new TransferCostTypeList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ICouponStatusList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new CouponStatusList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ITransactionTypeList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new TransactionTypeList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IEffectTypeList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new EffectTypeList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IStockEntityTypeList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new StockEntityTypeList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ICalculationMethodList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new CalculationMethodList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ISourceTypeList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new SourceTypeList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IStockFieldTypeList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new StockFieldTypeList(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<ICustomReportsList>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new CustomReportsList(config.ConnectionString, resourceSecurity);
        });
//Template_Component_RegisterList
    }

    public static void RegisterReports(this WebApplicationBuilder builder, BfsSettings? settings)
    {
        builder.Services.AddScoped<ITradingRoomRepCompare>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new TradingRoomRepCompare(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IPortfolioCompare>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new PortfolioCompare(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IPortfolioAggregateCompare>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new PortfolioAggregateCompare(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IPortfolioCashTransactionCompare>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new PortfolioCashTransactionCompare(config.ConnectionString, resourceSecurity);
        });
        builder.Services.AddScoped<IPortfolioCashTransactionAggregateCompare>(sp =>
        {
            var resourceSecurity = sp.GetRequiredService<IResourceSecurity>();
            var config = sp.GetRequiredService<TenantSqlConfiguration>();
            return new PortfolioCashTransactionAggregateCompare(config.ConnectionString, resourceSecurity);
        });
//Template_Component_RegisterReport
    }

    public static void RegisterClients(this WebApplicationBuilder builder, BfsSettings? settings)
    {
//Template_Component_RegisterHttpClient
    }
}
