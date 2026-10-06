using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IStockShareList
    {
        Task<QueryResponse<StockShareListItem>> GetAsync(QueryRequest<StockShareListFilter> request);
    }
}