using Bfs.Core.Contracts;
using Bfs.BestFit.Contracts;

namespace Bfs.BestFit.Domain.Interfaces
{
    public interface IReportsService
    {

        Task<QueryResponse<StructureReportItem>> StructureReportReportAsync(QueryRequest<StructureReportFilter> contractRequest);

        Task<QueryResponse<DataType1Item>> DataType1ReportAsync(QueryRequest<DataType1Filter> contractRequest);

//Template_Component_AddIServiceEntry
  }
}
