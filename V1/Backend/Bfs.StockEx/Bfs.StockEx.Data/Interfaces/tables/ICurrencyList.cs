using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ICurrencyList
    {
        Task<QueryResponse<CurrencyListItem>> GetAsync(QueryRequest<CurrencyListFilter> request);
    }
}

