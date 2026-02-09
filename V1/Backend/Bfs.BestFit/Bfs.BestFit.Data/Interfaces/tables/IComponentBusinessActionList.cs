using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface IComponentBusinessActionList
    {
        Task<QueryResponse<ComponentBusinessActionListItem>> GetComponentBusinessActionListAsync(QueryRequest<ComponentBusinessActionListFilter> request);
    }
}