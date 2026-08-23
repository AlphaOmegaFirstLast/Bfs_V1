using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.Master.Contracts;

namespace Bfs.Master.Domain.Interfaces
{
    public interface IBusinessActionService: ICrudService<BusinessAction>
    {
        Task<BusinessAction> UploadAsync(BusinessAction contract);

        Task<QueryResponse<BusinessActionListItem>> ListAsync(QueryRequest<BusinessActionListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

