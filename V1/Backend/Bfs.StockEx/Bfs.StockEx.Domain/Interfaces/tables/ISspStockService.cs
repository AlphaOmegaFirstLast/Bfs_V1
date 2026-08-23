using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ISspStockService: ICrudService<SspStock>
    {
        Task<SspStock> UploadAsync(SspStock contract);

        Task<QueryResponse<SspStockListItem>> ListAsync(QueryRequest<SspStockListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
