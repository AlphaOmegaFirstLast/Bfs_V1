using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ISsPortfolioBalanceList
    {
        Task<QueryResponse<SsPortfolioBalanceListItem>> GetAsync(QueryRequest<SsPortfolioBalanceListFilter> request);
    }
}

