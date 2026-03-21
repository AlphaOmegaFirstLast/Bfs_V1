using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IAuthRoleService
    {
        Task<AuthRole?> GetAsync(long id);
        Task<List<AuthRole>> GetAsync();

        Task<AuthRole> CreateAsync(AuthRole contract);
        Task<AuthRole?> UpdateAsync(AuthRole contract);
        Task DeleteAsync(long id);
        Task<AuthRole> UploadAsync(AuthRole contract);

        Task<QueryResponse<AuthRoleListItem>> ListAsync(QueryRequest<AuthRoleListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
