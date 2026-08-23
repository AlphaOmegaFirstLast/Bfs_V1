using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ISspTransactionList
    {
        Task<QueryResponse<SspTransactionListItem>> GetAsync(QueryRequest<SspTransactionListFilter> request);
    }
}

