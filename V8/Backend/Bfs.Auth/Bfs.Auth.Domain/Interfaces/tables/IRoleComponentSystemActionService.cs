using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Auth.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IRoleComponentSystemActionService: ICrudService<RoleComponentSystemAction>
    {
        Task<RoleComponentSystemAction> UploadAsync(RoleComponentSystemAction contract);

        Task<QueryResponse<RoleComponentSystemActionListItem>> ListAsync(QueryRequest<RoleComponentSystemActionListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
