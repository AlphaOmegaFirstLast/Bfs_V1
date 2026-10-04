using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IDebitInterestList
    {
        Task<QueryResponse<DebitInterestListItem>> GetAsync(QueryRequest<DebitInterestListFilter> request);
    }
}