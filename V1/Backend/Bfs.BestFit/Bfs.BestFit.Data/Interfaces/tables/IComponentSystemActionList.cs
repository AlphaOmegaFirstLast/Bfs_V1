using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface IComponentSystemActionList
    {
        Task<QueryResponse<ComponentSystemActionListItem>> GetComponentSystemActionListAsync(QueryRequest<ComponentSystemActionListFilter> request);
    }
}