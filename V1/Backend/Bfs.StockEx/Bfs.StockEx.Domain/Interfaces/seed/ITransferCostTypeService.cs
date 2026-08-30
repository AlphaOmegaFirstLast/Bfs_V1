using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ITransferCostTypeService: ICrudService<TransferCostType>
    {
        Task<TransferCostType> UploadAsync(TransferCostType contract);

        Task<QueryResponse<TransferCostTypeListItem>> ListAsync(QueryRequest<TransferCostTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

