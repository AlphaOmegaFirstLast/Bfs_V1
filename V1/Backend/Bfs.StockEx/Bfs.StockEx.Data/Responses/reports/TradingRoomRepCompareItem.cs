using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class TradingRoomRepCompareItem
    {
        public string? TradingRoom_Id { get; set; }
public string? TradingRoom_Name { get; set; }
public string? TradingRoom_Notes { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}

