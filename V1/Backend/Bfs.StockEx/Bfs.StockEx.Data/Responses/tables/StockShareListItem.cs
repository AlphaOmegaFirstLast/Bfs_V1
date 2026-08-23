using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class StockShareListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? TradingRoomId { get; set; }
public string? CurrencyId { get; set; }

        public string? TradingRoomName { get; set; }
public string? CurrencyName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}