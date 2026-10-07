using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Auth.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IRoleService: ICrudService<Role>
    {
        Task<Role> UploadAsync(Role contract);

        Task<QueryResponse<RoleListItem>> ListAsync(QueryRequest<RoleListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
