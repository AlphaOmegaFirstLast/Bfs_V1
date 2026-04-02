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
        public static List<TenantEntity> GetAllTenants(string masterDbConnection)
        {
           var options = new DbContextOptionsBuilder<MasterBasicDbContext>()
                .UseSqlServer(masterDbConnection) // Replace with your actual connection string
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

        public static void LoadTenants(WebApplication app, string masterDbConnection)
        {
            // preload tenant connection strings
            using (var scope = app.Services.CreateScope())
            {
                var cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

                // load teanants from database and cache them
                var allTenants = GetAllTenants(masterDbConnection);

                foreach (var tenant in allTenants)
                {
                    var memoryOptions = new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) };
                    cache.Set(tenant.TenantId.ToString(), tenant.DbConnection, memoryOptions);
                }
            }
        }

        public static void ApplyMigrations<T>(IServiceProvider services, string masterDbConnection) where T : DbContext
        {
            var tenants = GetAllTenants(masterDbConnection);
            // returns: IEnumerable<(string TenantId, string ConnectionString)>

            foreach (var tenant in tenants)
            {
                Console.WriteLine($"Migrating tenant: {tenant.TenantId}");

                var factory = new TenantDbFactory<T>(tenant.DbConnection);

                using var db = factory.Create();
                db.Database.Migrate();

                Console.WriteLine($"Tenant {tenant.TenantId} migration complete");
            }
        }


    }
}
