using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class TradingRoomListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}