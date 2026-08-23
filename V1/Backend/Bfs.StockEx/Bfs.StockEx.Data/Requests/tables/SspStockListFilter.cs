using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class SspStockListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? SsPortfolioId { get; set; }
public long? StockShareId { get; set; }

        public NumericRange? Quantity { get; set; }
public NumericRange? AverageCost { get; set; }

    }
}

