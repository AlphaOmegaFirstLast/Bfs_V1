using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IRoleComponentSystemActionService
    {
        Task<RoleComponentSystemAction?> GetAsync(long id);
        Task<List<RoleComponentSystemAction>> GetAsync();

        Task<RoleComponentSystemAction> CreateAsync(RoleComponentSystemAction contract);
        Task<RoleComponentSystemAction?> UpdateAsync(RoleComponentSystemAction contract);
        Task DeleteAsync(long id);
        Task<RoleComponentSystemAction> UploadAsync(RoleComponentSystemAction contract);

        Task<QueryResponse<RoleComponentSystemActionListItem>> ListAsync(QueryRequest<RoleComponentSystemActionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

