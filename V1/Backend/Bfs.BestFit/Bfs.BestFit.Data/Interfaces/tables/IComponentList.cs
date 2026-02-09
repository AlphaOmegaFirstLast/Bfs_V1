using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface IComponentList
    {
        Task<QueryResponse<ComponentListItem>> GetComponentListAsync(QueryRequest<ComponentListFilter> request);
    }
}