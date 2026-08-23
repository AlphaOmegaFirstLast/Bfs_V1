using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IOverdraftPortfolioList
    {
        Task<QueryResponse<OverdraftPortfolioListItem>> GetAsync(QueryRequest<OverdraftPortfolioListFilter> request);
    }
}