using Bfs.Core.Contracts;
using Bfs.Infrastructure.Contracts;

namespace Bfs.Infrastructure.Domain.Interfaces
{
    public interface IBfsTenantService
    {
        Task<BfsTenant?> GetAsync(long id);
        Task<List<BfsTenant>> GetAsync();

        Task<BfsTenant> CreateAsync(BfsTenant contract);
        Task<BfsTenant?> UpdateAsync(BfsTenant contract);
        Task DeleteAsync(long id);
        Task<BfsTenant> UploadAsync(BfsTenant contract);

        Task<QueryResponse<BfsTenantListItem>> ListAsync(QueryRequest<BfsTenantListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

