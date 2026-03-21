using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IStrProductList
    {
        Task<QueryResponse<StrProductListItem>> GetAsync(QueryRequest<StrProductListFilter> request);
    }
}