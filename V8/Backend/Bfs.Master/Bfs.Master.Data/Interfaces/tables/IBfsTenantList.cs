using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBfsTenantList
    {
        Task<QueryResponse<BfsTenantListItem>> GetAsync(QueryRequest<BfsTenantListFilter> request);
    }
}