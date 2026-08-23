using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ICashTransactionList
    {
        Task<QueryResponse<CashTransactionListItem>> GetAsync(QueryRequest<CashTransactionListFilter> request);
    }
}

