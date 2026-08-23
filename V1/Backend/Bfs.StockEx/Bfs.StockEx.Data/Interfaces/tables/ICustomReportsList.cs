using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ICustomReportsList
    {
        Task<QueryResponse<CustomReportsListItem>> GetAsync(QueryRequest<CustomReportsListFilter> request);
    }
}