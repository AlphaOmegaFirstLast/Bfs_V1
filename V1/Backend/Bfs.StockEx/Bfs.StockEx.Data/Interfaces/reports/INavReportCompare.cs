using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface INavReportCompare
    {
        Task<QueryResponse<NavReportCompareItem>> GetAsync(QueryRequest<NavReportCompareFilter> request);
    }
}

