using Bfs.Core.Config;
using Bfs.Core.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.TenantManagement
{
    public interface ITenantManager
    {
        string GetTenantDbConnection();
        string GetCacheKey();
        Task<List<TenantEntity>> FetchDataAsync(CancellationToken stoppingToken);
    }

    public class TenantManager : ITenantManager
    {
        private readonly IScopeData _scopeData;
        private readonly IMemoryCache _cache;
        private readonly BfsSettings _settings;


        public TenantManager(IOptions<BfsSettings> settings, IScopeData scopeData, IMemoryCache cache)
        {
            _settings = settings.Value;
            _scopeData = scopeData;
            _cache = cache;
        }

        public string GetTenantDbConnection()
        {
            var dbConnection = string.Empty;
            if (_settings.IsMigrationEnabled)
            {
                return dbConnection; // to stop DI from building Reports and Lists classes when migration is enabled, because they are not needed in migration process. As there is no CurrentTenant in migration process, so GetTenantDbConnection will return empty string, and the migration process will use MasterConnection. This is a workaround to avoid circular dependency, because Reports and Lists classes are used in migration process, but they are not needed in migration process, and they depend on ITenantProvider which will cause circular dependency.
            }
            if (_settings.IsMasterSystem)
            {
                dbConnection = _settings.DbConnections.MasterConnection ?? throw new Exception("Master connection string is not configured.");
            }
            else
            {
                var tenantId = _scopeData.TenantId;
                var cacheKey = GetCacheKey();

                if (_cache.TryGetValue(cacheKey, out List<TenantEntity>? tenantsData))
                    if (tenantsData != null)
                    {
                        var tenant = tenantsData.FirstOrDefault(t => t.TenantId == tenantId);
                        dbConnection = tenant?.DbConnection ?? string.Empty;
                    }
            }

            if (string.IsNullOrEmpty(dbConnection))
            {
                throw new Exception($"Tenant not found");
                // Simulate fetching tenant connection string from a data source
                //tenantConnectionString = "Server=localhost;Database=Tenant__Migrations;Trusted_Connection=True;TrustServerCertificate=True";
            }

            return dbConnection;
        }


        public string GetCacheKey()
        {
            return CacheKeys.Tenants; // You can customize this key based on your needs
        }

        // used in cache warming background service to fetch tenant data and store in cache, so that the first request can get tenant data from cache instead of database, which can improve performance and reduce database load.
        public async Task<List<TenantEntity>> FetchDataAsync(CancellationToken stoppingToken)
        {
            string masterConnection = _settings.DbConnections?.MasterConnection ?? throw new Exception("Master connection string is not configured.");
            var options = new DbContextOptionsBuilder<MasterBasicDbContext>()
                            .UseSqlServer(masterConnection) // Replace with your actual connection string
                            .Options;
            using var db = new MasterBasicDbContext(options);
            var allTenants = await db.BfsTenant
                     .Select(t => new TenantEntity
                     {
                         Id = t.Id,
                         TenantId = t.Id,
                         DbConnection = t.DbConnection
                     })
                     .ToListAsync();
            return allTenants;
        }

        public static List<TenantEntity> GetAllTenants(string masterConnection)
        {
            var options = new DbContextOptionsBuilder<MasterBasicDbContext>()
                 .UseSqlServer(masterConnection) // Replace with your actual connection string
                 .Options;
            using var db = new MasterBasicDbContext(options);
            return db.BfsTenant
                     .Select(t => new TenantEntity
                     {
                         Id = t.Id,
                         TenantId = t.Id,
                         DbConnection = t.DbConnection
                     })
                     .ToList();
        }

        private static async Task<List<TenantEntity>> GetTenantsOfSystem(string masterConnection, string systemName)
        {
            var options = new DbContextOptionsBuilder<MasterBasicDbContext>()
                 .UseSqlServer(masterConnection) // Replace with your actual connection string
                 .Options;

            // combine the three tables to get tenants of the specified system, combine in one query to avoid multiple database calls
            using var db = new MasterBasicDbContext(options);
            var query = from s in db.BfsSystem
                        join ts in db.BfsTenantSystem on s.Id equals ts.BfsSystemId
                        join t in db.BfsTenant on ts.BfsTenantId equals t.Id
                        where s.Name == systemName
                        select new TenantEntity
                        {
                            Id = t.Id,
                            TenantId = t.Id,
                            DbConnection = t.DbConnection
                        };
            return await query.ToListAsync();
        }

        public static async Task ApplyMigrations<T>(string masterConnection, string systemName) where T : DbContext
        {
            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    await db.Database.MigrateAsync();   // run DB migrations
            //    await SeedData.InitializeAsync(db); // seed initial data
            //}
            //ToDo Do we want to run migrations for all tenants at startup? Or should we have a separate background service that runs migrations for tenants on a schedule or when a new tenant is added? Running migrations for all tenants at startup could lead to longer startup times, especially if there are many tenants. A background service could help mitigate this by running migrations in the background without blocking the application startup.
            //ToDo Do we want to run migrations for all tenants or only tenants of the specified system? Running migrations for all tenants could lead to unnecessary migrations for tenants that are not part of the specified system, while running migrations only for tenants of the specified system could lead to faster migration times and less resource usage.
            var tenants = await GetTenantsOfSystem(masterConnection, systemName);

            foreach (var tenant in tenants)
            {
                try
                {
                    var factory = new TenantDbFactory<T>(tenant.DbConnection);

                    using var db = factory.Create();
                    db.Database.Migrate();

                    Console.WriteLine($"Tenant {tenant.TenantId} migration complete");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed: Migrating tenant: {tenant.TenantId}");
                }
            }
        }

    }
}
