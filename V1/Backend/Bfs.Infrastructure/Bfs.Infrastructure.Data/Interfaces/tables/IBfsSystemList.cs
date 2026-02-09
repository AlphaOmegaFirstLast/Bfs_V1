using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IBfsSystemList
    {
        Task<QueryResponse<BfsSystemListItem>> GetAsync(QueryRequest<BfsSystemListFilter> request);
    }
}