using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IActionLocationList
    {
        Task<QueryResponse<ActionLocationListItem>> GetAsync(QueryRequest<ActionLocationListFilter> request);
    }
}