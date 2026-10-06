using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBfsTenantSystemList
    {
        Task<QueryResponse<BfsTenantSystemListItem>> GetAsync(QueryRequest<BfsTenantSystemListFilter> request);
    }
}