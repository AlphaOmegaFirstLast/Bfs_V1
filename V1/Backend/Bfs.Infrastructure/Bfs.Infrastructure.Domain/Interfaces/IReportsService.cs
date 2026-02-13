using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface IReportsService
    {
        Task<QueryResponse<StructureCompareItem>> StructureCompareAsync(QueryRequest<StructureCompareFilter> contractRequest);

//Template_Component_AddIServiceEntry
  }
}
