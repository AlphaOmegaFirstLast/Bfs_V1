using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IBfsComponentList
    {
        Task<QueryResponse<BfsComponentListItem>> GetAsync(QueryRequest<BfsComponentListFilter> request);
    }
}