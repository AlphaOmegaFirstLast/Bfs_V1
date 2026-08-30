using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IStockEntityTypeList
    {
        Task<QueryResponse<StockEntityTypeListItem>> GetAsync(QueryRequest<StockEntityTypeListFilter> request);
    }
}

