using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBfsFieldService: ICrudService<BfsField>
    {
        Task<BfsField> UploadAsync(BfsField contract);

        Task<QueryResponse<BfsFieldListItem>> ListAsync(QueryRequest<BfsFieldListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

