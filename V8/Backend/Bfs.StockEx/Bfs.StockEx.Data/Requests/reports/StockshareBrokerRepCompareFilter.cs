using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class StockshareBrokerRepCompareFilter
    {
        public long? Stockshare_Id { get; set; }

        public string? Stockshare_Name { get; set; }

        public long? StockShare_TradingRoomId { get; set; }
public long? TradingRoom_BrokerId { get; set; }

    }
}