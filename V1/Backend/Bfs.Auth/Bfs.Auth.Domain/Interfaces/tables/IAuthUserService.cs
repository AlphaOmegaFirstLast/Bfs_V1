using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IAuthUserService
    {
        Task<AuthUser?> GetAsync(long id);
        Task<List<AuthUser>> GetAsync();

        Task<AuthUser> CreateAsync(AuthUser contract);
        Task<AuthUser?> UpdateAsync(AuthUser contract);
        Task DeleteAsync(long id);
        Task<AuthUser> UploadAsync(AuthUser contract);

        Task<QueryResponse<AuthUserListItem>> ListAsync(QueryRequest<AuthUserListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

