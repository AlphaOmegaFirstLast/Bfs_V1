using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class PortfolioCashTransactionAggregateCompareFilter
    {

        public string? SsPortfolio_Name { get; set; }

        public NumericRange? sumValue { get; set; }

    }
}