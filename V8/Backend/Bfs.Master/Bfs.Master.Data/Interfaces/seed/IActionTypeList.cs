using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IActionTypeList
    {
        Task<QueryResponse<ActionTypeListItem>> GetAsync(QueryRequest<ActionTypeListFilter> request);
    }
}