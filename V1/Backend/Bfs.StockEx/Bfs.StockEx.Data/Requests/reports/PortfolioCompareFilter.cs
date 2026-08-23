using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class PortfolioCompareFilter
    {

        public string? SsPortfolio_Name { get; set; }
public string? StockShare_Name { get; set; }

        public NumericRange? SspTransaction_Quantity { get; set; }
public NumericRange? SspTransaction_Price { get; set; }
public DateRange? SspTransaction_TransactionDate { get; set; }

    }
}

