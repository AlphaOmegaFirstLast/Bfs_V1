using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IStoreList
    {
        Task<QueryResponse<StoreListItem>> GetAsync(QueryRequest<StoreListFilter> request);
    }
}

