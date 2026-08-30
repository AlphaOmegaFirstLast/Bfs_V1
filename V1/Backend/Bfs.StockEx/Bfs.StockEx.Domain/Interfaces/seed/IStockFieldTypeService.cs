using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IStockFieldTypeService: ICrudService<StockFieldType>
    {
        Task<StockFieldType> UploadAsync(StockFieldType contract);

        Task<QueryResponse<StockFieldTypeListItem>> ListAsync(QueryRequest<StockFieldTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

