using Bfs.Core.Data;
using Bfs.Stores.Data;

namespace Bfs.Stores.Data.Interfaces
{
    public interface ICurrencyList
    {
        Task<QueryResponse<CurrencyListItem>> GetAsync(QueryRequest<CurrencyListFilter> request);
    }
}

