using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface IStrTransactionList
    {
        Task<QueryResponse<StrTransactionListItem>> GetAsync(QueryRequest<StrTransactionListFilter> request);
    }
}