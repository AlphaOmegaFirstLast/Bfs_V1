using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class BrokerListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public string Code { get; set; }
public string Email { get; set; }
public long TradingRoomId { get; set; }

        public string? TradingRoomName { get; set; }

   }
}