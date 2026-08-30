using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBfsComponentService: ICrudService<BfsComponent>
    {
        Task<BfsComponent> UploadAsync(BfsComponent contract);

        Task<QueryResponse<BfsComponentListItem>> ListAsync(QueryRequest<BfsComponentListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

