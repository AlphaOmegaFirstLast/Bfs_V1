using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.TenantManagement
{
    public class TenantDbFactory<T> where T : DbContext
    {
        private readonly DbContextOptions<T> _options;

        public TenantDbFactory(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlServer(connectionString, sql =>
            {
                sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });

            _options = builder.Options;
        }

        //Use reflection to call the correct constructor
        public T Create() => (T)Activator.CreateInstance(typeof(T), _options)!;
    }
}
