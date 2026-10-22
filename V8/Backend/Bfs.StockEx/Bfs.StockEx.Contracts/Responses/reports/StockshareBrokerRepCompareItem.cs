using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class StockshareBrokerRepCompareItem
    {
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public string? Id { get; set; }
        public string? Stockshare_Id { get; set; }
        public string? Stockshare_Name { get; set; }
public string? StockShare_TradingRoomId { get; set; }
public string? TradingRoom_BrokerId { get; set; }

        public string? TradingRoomName { get; set; }
public string? BrokerName { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}