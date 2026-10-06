using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class PortfolioCashTransactionAggregateCompareFilter
    {

        public string? SsPortfolio_Name { get; set; }

        public NumericRange? sumValue { get; set; }

    }
}