using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBfsSystemList
    {
        Task<QueryResponse<BfsSystemListItem>> GetAsync(QueryRequest<BfsSystemListFilter> request);
    }
}