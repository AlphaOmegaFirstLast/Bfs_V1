using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
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