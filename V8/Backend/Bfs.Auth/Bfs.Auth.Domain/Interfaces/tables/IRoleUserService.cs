using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Auth.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IRoleUserService: ICrudService<RoleUser>
    {
        Task<RoleUser> UploadAsync(RoleUser contract);

        Task<QueryResponse<RoleUserListItem>> ListAsync(QueryRequest<RoleUserListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
