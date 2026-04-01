using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Core.TenantManagement
{
    public class TenantEntity : IIdentifiable, ITenanted
    {
        public long TenantId { get; set; }
        public string order { get; set; } // UI selection, so the tenant id is not exposed to the user
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string DbConnection { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}


