using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IProductList
    {
        Task<QueryResponse<ProductListItem>> GetAsync(QueryRequest<ProductListFilter> request);
    }
}