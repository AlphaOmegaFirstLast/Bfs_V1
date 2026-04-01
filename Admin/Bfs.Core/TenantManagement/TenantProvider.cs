using Bfs.Core.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.TenantManagement
{
    public interface ITenantProvider
    {
        string GetCurrentTenantDbConnection();
        string GetCurrentTenantId();
    }

    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;
        private readonly BfsSettings _settings;


        public TenantProvider(IOptions<BfsSettings> settings, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _settings = settings.Value;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public string GetCurrentTenantId()
        {
            //Only if the api runs as a sub domain. you can get tenantId from "refresh-token" cookie. refreshToken="tenantId|userId".
            //var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refresh-token"]?.ToString();
            //var tenantId = refreshToken?.Split('|').FirstOrDefault();

            // get tenantId from user claims, the claim type is "TenantId", the claim value is tenantId
            var tenantId = _httpContextAccessor.HttpContext?
                .User.Claims.FirstOrDefault(c => c.Type == "tenantId")?
                .Value;

            if (tenantId == null)
            {
                throw new Exception("Tenant ID not found in refresh token");
            }

            return tenantId;
        }

        public string GetCurrentTenantDbConnection()
        {
            if (_settings.IsMasterSystem)
            {
                return _settings.DbConnections?.MasterConnection ?? throw new Exception("Master connection string is not configured.");
            }
            else 
            {
                var tenantId = GetCurrentTenantId();
                if (!_cache.TryGetValue(tenantId, out string tenantConnectionString))
                {
                    throw new Exception($"Tenant not found: {tenantId}");
                }

                // Simulate fetching tenant connection string from a data source
                //tenantConnectionString = "Server=localhost;Database=Tenant10;Trusted_Connection=True;TrustServerCertificate=True";

                return tenantConnectionString;
            }
        }
    }
}
