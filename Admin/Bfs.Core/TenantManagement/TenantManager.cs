using Bfs.Core.Services.Auth;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.TenantManagement
{
    public class TenantManager
    {
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

        public static async Task<List<TenantEntity>> GetTenantsOfSystem(string masterConnection, string systemName)
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

        public static void LoadTenants(WebApplication app, string masterConnection)
        {
            // preload tenant connection strings
            using (var scope = app.Services.CreateScope())
            {
                var cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

                // load teanants from database and cache them
                var allTenants = GetAllTenants(masterConnection);

                foreach (var tenant in allTenants)
                {
                    var memoryOptions = new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) };
                    cache.Set(tenant.TenantId.ToString(), tenant.DbConnection, memoryOptions);
                }
            }
        }

        public static async Task ApplyMigrations<T>(string masterConnection, string systemName) where T : DbContext
        {
            var tenants = await GetTenantsOfSystem(masterConnection, systemName);

            foreach (var tenant in tenants)
            {
                try {
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
