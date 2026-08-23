using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class PortfolioAggregateCompareFilter
    {

        public string? SsPortfolio_Name { get; set; }

        public NumericRange? sumQuantity { get; set; }
public NumericRange? sumPrice { get; set; }

    }
}

