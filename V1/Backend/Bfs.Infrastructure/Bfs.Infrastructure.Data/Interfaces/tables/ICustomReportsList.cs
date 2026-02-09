using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface ICustomReportsList
    {
        Task<QueryResponse<CustomReportsListItem>> GetAsync(QueryRequest<CustomReportsListFilter> request);
    }
}