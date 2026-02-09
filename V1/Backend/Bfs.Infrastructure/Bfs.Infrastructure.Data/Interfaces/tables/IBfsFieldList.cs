using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IBfsFieldList
    {
        Task<QueryResponse<BfsFieldListItem>> GetAsync(QueryRequest<BfsFieldListFilter> request);
    }
}