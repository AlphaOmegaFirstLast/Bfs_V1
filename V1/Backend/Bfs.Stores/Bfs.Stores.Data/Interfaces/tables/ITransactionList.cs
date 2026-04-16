using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface ITransactionList
    {
        Task<QueryResponse<TransactionListItem>> GetAsync(QueryRequest<TransactionListFilter> request);
    }
}