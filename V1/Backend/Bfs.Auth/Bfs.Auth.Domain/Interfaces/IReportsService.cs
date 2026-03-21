using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IReportsService
    {

        Task<QueryResponse<RoleRepCompareItem>> RoleRepCompareAsync(QueryRequest<RoleRepCompareFilter> contractRequest);

//Template_Component_AddIServiceEntry
  }
}
