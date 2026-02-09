using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IStructureReportReport
    {
        Task<QueryResponse<StructureReportItem>> GetAsync(QueryRequest<StructureReportFilter> request);
    }
}