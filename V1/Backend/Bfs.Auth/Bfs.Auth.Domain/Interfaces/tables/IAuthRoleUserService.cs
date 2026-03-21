using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IAuthRoleUserService
    {
        Task<AuthRoleUser?> GetAsync(long id);
        Task<List<AuthRoleUser>> GetAsync();

        Task<AuthRoleUser> CreateAsync(AuthRoleUser contract);
        Task<AuthRoleUser?> UpdateAsync(AuthRoleUser contract);
        Task DeleteAsync(long id);
        Task<AuthRoleUser> UploadAsync(AuthRoleUser contract);

        Task<QueryResponse<AuthRoleUserListItem>> ListAsync(QueryRequest<AuthRoleUserListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
