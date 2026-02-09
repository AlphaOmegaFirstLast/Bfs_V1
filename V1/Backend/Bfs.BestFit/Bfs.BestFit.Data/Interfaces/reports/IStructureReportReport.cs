using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface IStructureReportReport
    {
        Task<QueryResponse<StructureReportItem>> GetAsync(QueryRequest<StructureReportFilter> request);
    }
}