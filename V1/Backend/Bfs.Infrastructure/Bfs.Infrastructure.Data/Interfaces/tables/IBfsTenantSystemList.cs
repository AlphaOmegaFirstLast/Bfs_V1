using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IBfsTenantSystemList
    {
        Task<QueryResponse<BfsTenantSystemListItem>> GetAsync(QueryRequest<BfsTenantSystemListFilter> request);
    }
}