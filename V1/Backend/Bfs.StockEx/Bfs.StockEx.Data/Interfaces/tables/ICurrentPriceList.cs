using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ICurrentPriceList
    {
        Task<QueryResponse<CurrentPriceListItem>> GetAsync(QueryRequest<CurrentPriceListFilter> request);
    }
}