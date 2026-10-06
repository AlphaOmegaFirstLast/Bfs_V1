using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class PortfolioCashTransactionCompareFilter
    {

        public string? SsPortfolio_Name { get; set; }

        public NumericRange? CashTransaction_Value { get; set; }
public DateRange? CashTransaction_TransactionDate { get; set; }

    }
}