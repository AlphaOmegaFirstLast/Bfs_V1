using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ITradingRoomRepCompare
    {
        Task<QueryResponse<TradingRoomRepCompareItem>> GetAsync(QueryRequest<TradingRoomRepCompareFilter> request);
    }
}

