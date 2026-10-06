using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IStockShareService: ICrudService<StockShare>
    {
        Task<StockShare> UploadAsync(StockShare contract);

        Task<QueryResponse<StockShareListItem>> ListAsync(QueryRequest<StockShareListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
