using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ITransactionTypeList
    {
        Task<QueryResponse<TransactionTypeListItem>> GetAsync(QueryRequest<TransactionTypeListFilter> request);
    }
}

