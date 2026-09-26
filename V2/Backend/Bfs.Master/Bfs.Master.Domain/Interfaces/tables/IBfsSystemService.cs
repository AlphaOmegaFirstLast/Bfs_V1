using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBfsSystemService: ICrudService<BfsSystem>
    {
        Task<BfsSystem> UploadAsync(BfsSystem contract);

        Task<QueryResponse<BfsSystemListItem>> ListAsync(QueryRequest<BfsSystemListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

    }
}
