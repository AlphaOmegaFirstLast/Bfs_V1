using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IStockEntityTypeService: ICrudService<StockEntityType>
    {
        Task<StockEntityType> UploadAsync(StockEntityType contract);

        Task<QueryResponse<StockEntityTypeListItem>> ListAsync(QueryRequest<StockEntityTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

