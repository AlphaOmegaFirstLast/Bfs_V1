using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
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

