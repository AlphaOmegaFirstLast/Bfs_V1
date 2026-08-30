using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IAreaList
    {
        Task<QueryResponse<AreaListItem>> GetAsync(QueryRequest<AreaListFilter> request);
    }
}