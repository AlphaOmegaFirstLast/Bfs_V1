using Bfs.Core.Contracts;
using Bfs.Master.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1
namespace Bfs.Master.Domain.Interfaces
{
    public interface IReportsService
    {

        Task<QueryResponse<StructureCompareItem>> StructureCompareAsync(QueryRequest<StructureCompareFilter> contractRequest);

//Template_Component_AddIServiceEntry
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
  }
}
