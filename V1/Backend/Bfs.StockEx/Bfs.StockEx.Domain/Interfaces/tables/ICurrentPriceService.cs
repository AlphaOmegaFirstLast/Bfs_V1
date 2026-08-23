using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ICurrentPriceService: ICrudService<CurrentPrice>
    {
        Task<CurrentPrice> UploadAsync(CurrentPrice contract);

        Task<QueryResponse<CurrentPriceListItem>> ListAsync(QueryRequest<CurrentPriceListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
