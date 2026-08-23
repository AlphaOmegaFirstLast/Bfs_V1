using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IStockShareService: ICrudService<StockShare>
    {
        Task<StockShare> UploadAsync(StockShare contract);

        Task<QueryResponse<StockShareListItem>> ListAsync(QueryRequest<StockShareListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
