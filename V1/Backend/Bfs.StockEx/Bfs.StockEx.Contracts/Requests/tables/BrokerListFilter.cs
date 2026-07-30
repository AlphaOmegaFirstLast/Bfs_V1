using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class BrokerListFilter
    {

        public string? Code { get; set; }
public string? Name { get; set; }

        public long? TradingRoomId { get; set; }

    }
}