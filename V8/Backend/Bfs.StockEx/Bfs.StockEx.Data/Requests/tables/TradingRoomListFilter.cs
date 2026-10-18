using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class TradingRoomListFilter
    {
        public bool? IsAutoComplete { get; set; } = false ;

        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? CurrencyId { get; set; }

    }
}