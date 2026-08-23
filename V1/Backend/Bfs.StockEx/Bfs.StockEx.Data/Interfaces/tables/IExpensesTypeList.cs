using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IExpensesTypeList
    {
        Task<QueryResponse<ExpensesTypeListItem>> GetAsync(QueryRequest<ExpensesTypeListFilter> request);
    }
}