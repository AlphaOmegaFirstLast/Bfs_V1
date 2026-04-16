using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IProductTransactionCompare
    {
        Task<QueryResponse<ProductTransactionCompareItem>> GetAsync(QueryRequest<ProductTransactionCompareFilter> request);
    }
}

