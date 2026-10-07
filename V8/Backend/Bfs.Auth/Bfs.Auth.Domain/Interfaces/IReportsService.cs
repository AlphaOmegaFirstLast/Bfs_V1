using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1
namespace Bfs.Auth.Domain.Interfaces
{
    public interface IReportsService
    {

        Task<QueryResponse<RoleRepCompareItem>> RoleRepCompareAsync(QueryRequest<RoleRepCompareFilter> contractRequest);

//Template_Component_AddIServiceEntry
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
  }
}
