using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface ICustomReportsList
    {
        Task<QueryResponse<CustomReportsListItem>> GetCustomReportsListAsync(QueryRequest<CustomReportsListFilter> request);
    }
}