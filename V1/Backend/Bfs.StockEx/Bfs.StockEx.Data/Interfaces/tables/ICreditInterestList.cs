using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ICreditInterestList
    {
        Task<QueryResponse<CreditInterestListItem>> GetAsync(QueryRequest<CreditInterestListFilter> request);
    }
}