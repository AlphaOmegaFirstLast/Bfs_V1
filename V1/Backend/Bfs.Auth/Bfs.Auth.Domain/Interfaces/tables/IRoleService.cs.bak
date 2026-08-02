using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IRoleService
    {
        Task<Role?> GetAsync(long id);
        Task<List<Role>> GetAsync();

        Task<Role> CreateAsync(Role contract);
        Task<Role?> UpdateAsync(Role contract);
        Task DeleteAsync(long id);
        Task<Role> UploadAsync(Role contract);

        Task<QueryResponse<RoleListItem>> ListAsync(QueryRequest<RoleListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

