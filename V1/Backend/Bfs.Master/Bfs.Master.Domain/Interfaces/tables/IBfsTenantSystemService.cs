using Bfs.Core.Contracts;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBfsTenantSystemService
    {
        Task<BfsTenantSystem?> GetAsync(long id);
        Task<List<BfsTenantSystem>> GetAsync();

        Task<BfsTenantSystem> CreateAsync(BfsTenantSystem contract);
        Task<BfsTenantSystem?> UpdateAsync(BfsTenantSystem contract);
        Task DeleteAsync(long id);
        Task<BfsTenantSystem> UploadAsync(BfsTenantSystem contract);

        Task<QueryResponse<BfsTenantSystemListItem>> ListAsync(QueryRequest<BfsTenantSystemListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
