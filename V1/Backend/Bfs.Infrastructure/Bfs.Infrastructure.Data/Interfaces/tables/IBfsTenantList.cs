using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IBfsTenantList
    {
        Task<QueryResponse<BfsTenantListItem>> GetAsync(QueryRequest<BfsTenantListFilter> request);
    }
}