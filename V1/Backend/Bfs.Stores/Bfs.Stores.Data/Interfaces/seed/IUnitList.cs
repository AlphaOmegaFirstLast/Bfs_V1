using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IUnitList
    {
        Task<QueryResponse<UnitListItem>> GetAsync(QueryRequest<UnitListFilter> request);
    }
}

