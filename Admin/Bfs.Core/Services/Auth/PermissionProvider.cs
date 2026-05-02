using Bfs.Core.Config;
using Bfs.Core.TenantManagement;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Auth
{
    public interface IPermissionProvider
    {
        Task<IReadOnlyList<RoleMethod>> GetPermissionsAsync(
            string tenantId,
            string masterConnection,
            string tenantDbConnection);
    }

    public class PermissionProvider : IPermissionProvider
    {
        private readonly IMemoryCache _cache;

        public PermissionProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<IReadOnlyList<RoleMethod>> GetPermissionsAsync(
            string tenantId,
            string masterConnection,
            string tenantDbConnection)
        {
            var cacheKey = $"{CacheKeys.Permissions}:{tenantId}";

            return _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

                var list = await GetRoleMethodList(masterConnection, tenantDbConnection);
                return (IReadOnlyList<RoleMethod>)list;
            });
        }

        private static async Task<List<RoleMethod>> GetRoleMethodList(string masterConnection, string tenantConnection)
        {
            using var masterDb = new SqlConnection(masterConnection);
            var componentSelect = "Select ca.bfsComponentId, ca.systemActionId, (a.shortName + '.'+ c.Name) method " +
                " from bfsComponentSystemAction ca " +
                " inner join bfsComponent c on c.Id = ca.bfsComponentId " +
                " inner join SystemAction a on a.Id = ca.systemActionId ";

            var methods = await masterDb.QueryAsync(componentSelect.ToString(), null);
            var methodsList = methods.ToList();

            using var tenantDb = new SqlConnection(tenantConnection);
            var sqlSelect = "Select r.RoleId roleId, r.bfsComponentId, r.SystemActionId " +
                " from athRoleComponentSystemAction r ";
            var roleActions = await tenantDb.QueryAsync(sqlSelect.ToString(), null);
            var roleActionlist = roleActions.ToList();

            // Join the two lists to get the methods for the role, so that the output is List<(string RoleId, string Method)>
            var roleMethods = from m in methodsList
                              join ra in roleActionlist on m.bfsComponentId equals ra.bfsComponentId
                              where m.systemActionId == ra.SystemActionId
                              select new RoleMethod
                              {
                                  RoleId = ra.roleId,
                                  Method = m.method
                              };
            return roleMethods.ToList();
        }

    }

    public class RoleMethod
    {
        public dynamic RoleId { get; set; }
        public dynamic Method { get; set; }
    }
}
