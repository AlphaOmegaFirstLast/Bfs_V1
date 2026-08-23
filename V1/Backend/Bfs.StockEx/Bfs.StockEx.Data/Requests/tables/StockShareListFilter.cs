using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class StockShareListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? TradingRoomId { get; set; }
public long? CurrencyId { get; set; }

    }
}