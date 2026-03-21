using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IAuthRoleAppService
    {
        Task<AuthRoleApp?> GetAsync(long id);
        Task<List<AuthRoleApp>> GetAsync();

        Task<AuthRoleApp> CreateAsync(AuthRoleApp contract);
        Task<AuthRoleApp?> UpdateAsync(AuthRoleApp contract);
        Task DeleteAsync(long id);
        Task<AuthRoleApp> UploadAsync(AuthRoleApp contract);

        Task<QueryResponse<AuthRoleAppListItem>> ListAsync(QueryRequest<AuthRoleAppListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
