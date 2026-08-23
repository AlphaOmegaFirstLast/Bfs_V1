using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ITradingRoomService: ICrudService<TradingRoom>
    {
        Task<TradingRoom> UploadAsync(TradingRoom contract);

        Task<QueryResponse<TradingRoomListItem>> ListAsync(QueryRequest<TradingRoomListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
