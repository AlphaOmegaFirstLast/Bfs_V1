using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class SspTransactionListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? SsPortfolioId { get; set; }
public int? TransactionTypeId { get; set; }
public long? StockShareId { get; set; }
public long? ToPortfolioId { get; set; }

        public DateRange? SourceDate { get; set; }
public DateRange? TransactionDate { get; set; }
public NumericRange? Quantity { get; set; }
public NumericRange? Price { get; set; }
public NumericRange? ToQuantity { get; set; }

    }
}

