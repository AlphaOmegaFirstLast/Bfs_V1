using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ISsPortfolioList
    {
        Task<QueryResponse<SsPortfolioListItem>> GetAsync(QueryRequest<SsPortfolioListFilter> request);
    }
}

