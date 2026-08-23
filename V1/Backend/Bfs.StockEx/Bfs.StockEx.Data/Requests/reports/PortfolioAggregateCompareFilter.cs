using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class PortfolioAggregateCompareFilter
    {

        public string? SsPortfolio_Name { get; set; }

        public NumericRange? sumQuantity { get; set; }
public NumericRange? sumPrice { get; set; }

    }
}

