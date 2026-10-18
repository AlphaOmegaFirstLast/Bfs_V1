using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class TradingRoomListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public long CurrencyId { get; set; }

        public string? CurrencyName { get; set; }

   }
}