using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IRoleUserService
    {
        Task<RoleUser?> GetAsync(long id);
        Task<List<RoleUser>> GetAsync();

        Task<RoleUser> CreateAsync(RoleUser contract);
        Task<RoleUser?> UpdateAsync(RoleUser contract);
        Task DeleteAsync(long id);
        Task<RoleUser> UploadAsync(RoleUser contract);

        Task<QueryResponse<RoleUserListItem>> ListAsync(QueryRequest<RoleUserListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

