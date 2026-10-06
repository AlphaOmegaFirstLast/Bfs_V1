using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ITransferCostTypeService: ICrudService<TransferCostType>
    {
        Task<TransferCostType> UploadAsync(TransferCostType contract);

        Task<QueryResponse<TransferCostTypeListItem>> ListAsync(QueryRequest<TransferCostTypeListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
