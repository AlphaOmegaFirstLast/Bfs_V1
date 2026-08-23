using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IPortfolioCompare
    {
        Task<QueryResponse<PortfolioCompareItem>> GetAsync(QueryRequest<PortfolioCompareFilter> request);
    }
}

