using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IAuthRoleComponentSystemActionService
    {
        Task<AuthRoleComponentSystemAction?> GetAsync(long id);
        Task<List<AuthRoleComponentSystemAction>> GetAsync();

        Task<AuthRoleComponentSystemAction> CreateAsync(AuthRoleComponentSystemAction contract);
        Task<AuthRoleComponentSystemAction?> UpdateAsync(AuthRoleComponentSystemAction contract);
        Task DeleteAsync(long id);
        Task<AuthRoleComponentSystemAction> UploadAsync(AuthRoleComponentSystemAction contract);

        Task<QueryResponse<AuthRoleComponentSystemActionListItem>> ListAsync(QueryRequest<AuthRoleComponentSystemActionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
