using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class CurrentPriceListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? StockShareId { get; set; }

        public DateRange? TransactionDate { get; set; }
public NumericRange? Price { get; set; }

    }
}