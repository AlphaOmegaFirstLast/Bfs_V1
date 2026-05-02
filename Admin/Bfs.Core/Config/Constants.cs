using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Config
{
    public static class BfsDefault
    {
        public const string TenantId = "1";
        public const string BfsAdminRoleId = "1";
        public const string IdentityRoleId = "2";
        public const string ClientAdminRoleId = "3";
    }

    public static class CacheKeys
    {
        public const string Tenants = "tenants";
        public const string Permissions = "permissions";
    }

    public enum RequestStatus
    {
        WaitingApproval = 1,
        Approved = 2,
        Rejected = 3,
    }
}
