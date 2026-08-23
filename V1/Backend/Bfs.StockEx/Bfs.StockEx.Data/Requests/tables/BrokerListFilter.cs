using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class BrokerListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? TradingRoomId { get; set; }

    }
}