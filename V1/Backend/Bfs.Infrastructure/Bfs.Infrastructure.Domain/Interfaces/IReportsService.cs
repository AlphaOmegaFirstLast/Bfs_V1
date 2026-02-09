using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface IReportsService
    {

        Task<QueryResponse<StructureReportItem>> StructureReportReportAsync(QueryRequest<StructureReportFilter> contractRequest);

//Template_Component_AddIServiceEntry
  }
}
