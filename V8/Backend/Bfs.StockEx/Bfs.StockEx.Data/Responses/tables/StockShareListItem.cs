using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class StockShareListItem
    {      
        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public long TradingRoomId { get; set; }
public long CurrencyId { get; set; }

        public string? TradingRoomName { get; set; }
public string? CurrencyName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}