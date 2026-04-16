using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBfsComponentList
    {
        Task<QueryResponse<BfsComponentListItem>> GetAsync(QueryRequest<BfsComponentListFilter> request);
    }
}