using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IReportCompare
    {
        Task<QueryResponse<ReportCompareItem>> GetAsync(QueryRequest<ReportCompareFilter> request);
    }
}