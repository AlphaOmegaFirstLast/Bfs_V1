using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ISspStockList
    {
        Task<QueryResponse<SspStockListItem>> GetAsync(QueryRequest<SspStockListFilter> request);
    }
}

