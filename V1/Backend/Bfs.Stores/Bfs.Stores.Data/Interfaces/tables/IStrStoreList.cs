using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IStrStoreList
    {
        Task<QueryResponse<StrStoreListItem>> GetAsync(QueryRequest<StrStoreListFilter> request);
    }
}