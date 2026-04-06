using Bfs.Core.Contracts;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Domain.Interfaces
{
    public interface IRoleAppService
    {
        Task<RoleApp?> GetAsync(long id);
        Task<List<RoleApp>> GetAsync();

        Task<RoleApp> CreateAsync(RoleApp contract);
        Task<RoleApp?> UpdateAsync(RoleApp contract);
        Task DeleteAsync(long id);
        Task<RoleApp> UploadAsync(RoleApp contract);

        Task<QueryResponse<RoleAppListItem>> ListAsync(QueryRequest<RoleAppListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

