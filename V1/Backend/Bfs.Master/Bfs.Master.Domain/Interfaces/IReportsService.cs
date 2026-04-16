using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IReportsService
    {

        Task<QueryResponse<StructureCompareItem>> StructureCompareAsync(QueryRequest<StructureCompareFilter> contractRequest);

//Template_Component_AddIServiceEntry
  }
}
