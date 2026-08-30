using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Stores.Contracts;

namespace Bfs.Stores.Domain.Interfaces
{
    public interface IOperationService: ICrudService<Operation>
    {
        Task<Operation> UploadAsync(Operation contract);

        Task<QueryResponse<OperationListItem>> ListAsync(QueryRequest<OperationListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

