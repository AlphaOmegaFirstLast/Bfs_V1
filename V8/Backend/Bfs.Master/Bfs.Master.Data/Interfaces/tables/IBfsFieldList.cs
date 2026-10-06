using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBfsFieldList
    {
        Task<QueryResponse<BfsFieldListItem>> GetAsync(QueryRequest<BfsFieldListFilter> request);
    }
}