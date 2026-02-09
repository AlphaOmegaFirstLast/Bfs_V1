using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IBfsClientList
    {
        Task<QueryResponse<BfsClientListItem>> GetAsync(QueryRequest<BfsClientListFilter> request);
    }
}