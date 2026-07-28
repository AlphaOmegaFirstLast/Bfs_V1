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
                {
                    if (tenantsData != null)
                    {
                        var tenant = tenantsData.FirstOrDefault(t => t.TenantId == tenantId);
                        dbConnection = tenant?.DbConnection ?? string.Empty;
                    }
                    else 
                    {
                        throw new Exception($"No Tenants found");
                    }
                }
                else
                {
                    throw new Exception($"Tenants data not found in cache");
                }
            }

            if (string.IsNullOrEmpty(dbConnection))
            {
                if (_settings.IsSecurityEnabled)
                {
                    throw new Exception($"Tenant not found");
                }
                else
                {
                    // Simulate fetching tenant connection string from a data source
                    dbConnection = _settings.DbConnections.TenantTestConnection ?? throw new Exception("Test tenant connection string is not configured.");
                }
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

        private static async Task<List<TenantEntity>> GetAllTenants(string masterConnection, string systemName)
        {
            var options = new DbContextOptionsBuilder<MasterBasicDbContext>()
                 .UseSqlServer(masterConnection) // Replace with your actual connection string
                 .Options;

            // combine the three tables to get tenants of the specified system, combine in one query to avoid multiple database calls
            using var db = new MasterBasicDbContext(options);
            var query = from t in db.BfsTenant 
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
            // ToDo Apply SeedData after migration, but SeedData needs to use tenant db context, so it will cause circular dependency if we put SeedData in this project, need to find a way to avoid circular dependency, maybe we can put SeedData in a separate project and reference it in both projects, or we can use reflection to call SeedData without referencing it directly. For now, we can run SeedData manually after running migration, as the number of tenants is not large, it should not be a big issue.
            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //    await db.Database.MigrateAsync();   // run DB migrations
            //    await SeedData.InitializeAsync(db); // seed initial data
            //}

            var isApplyMigrationsForAllTenants = true; // Set this flag based on your requirements
            var tenants = isApplyMigrationsForAllTenants 
                ? await GetAllTenants(masterConnection, systemName) 
                : await GetTenantsOfSystem(masterConnection, systemName);

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
