using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class PortfolioCashTransactionCompareFilter
    {

        public string? SsPortfolio_Name { get; set; }

        public NumericRange? CashTransaction_Value { get; set; }
public DateRange? CashTransaction_TransactionDate { get; set; }

    }
}