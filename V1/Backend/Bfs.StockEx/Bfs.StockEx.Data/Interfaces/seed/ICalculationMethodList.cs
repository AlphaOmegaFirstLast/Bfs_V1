using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ICalculationMethodList
    {
        Task<QueryResponse<CalculationMethodListItem>> GetAsync(QueryRequest<CalculationMethodListFilter> request);
    }
}

