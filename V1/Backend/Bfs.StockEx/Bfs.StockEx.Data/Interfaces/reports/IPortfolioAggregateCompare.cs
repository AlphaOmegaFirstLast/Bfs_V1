using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IPortfolioAggregateCompare
    {
        Task<QueryResponse<PortfolioAggregateCompareItem>> GetAsync(QueryRequest<PortfolioAggregateCompareFilter> request);
    }
}

