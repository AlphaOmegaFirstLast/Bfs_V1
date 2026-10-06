using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IStockFieldTypeService: ICrudService<StockFieldType>
    {
        Task<StockFieldType> UploadAsync(StockFieldType contract);

        Task<QueryResponse<StockFieldTypeListItem>> ListAsync(QueryRequest<StockFieldTypeListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
