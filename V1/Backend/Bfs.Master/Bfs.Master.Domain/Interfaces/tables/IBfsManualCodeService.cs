using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBfsManualCodeService: ICrudService<BfsManualCode>
    {
        Task<BfsManualCode> UploadAsync(BfsManualCode contract);

        Task<QueryResponse<BfsManualCodeListItem>> ListAsync(QueryRequest<BfsManualCodeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
