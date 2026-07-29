using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ITradingRoomList
    {
        Task<QueryResponse<TradingRoomListItem>> GetAsync(QueryRequest<TradingRoomListFilter> request);
    }
}

