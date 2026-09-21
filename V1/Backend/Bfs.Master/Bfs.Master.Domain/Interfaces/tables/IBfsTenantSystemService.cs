using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBfsTenantSystemService: ICrudService<BfsTenantSystem>
    {
        Task<BfsTenantSystem> UploadAsync(BfsTenantSystem contract);

        Task<QueryResponse<BfsTenantSystemListItem>> ListAsync(QueryRequest<BfsTenantSystemListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

