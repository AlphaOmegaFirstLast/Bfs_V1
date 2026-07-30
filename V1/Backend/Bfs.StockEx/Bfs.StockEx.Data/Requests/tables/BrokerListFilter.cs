using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class BrokerListFilter
    {

        public string? Code { get; set; }
public string? Name { get; set; }

        public long? TradingRoomId { get; set; }

    }
}