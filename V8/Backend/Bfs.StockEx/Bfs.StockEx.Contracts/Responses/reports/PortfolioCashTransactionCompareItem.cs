using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class PortfolioCashTransactionCompareItem
    {
        public string? SsPortfolio_Name { get; set; }
public string? Broker_Name { get; set; }
public string? Investor_Name { get; set; }
public string? CashTransaction_Value { get; set; }
public string? CashTransaction_TransactionDate { get; set; }
public string? TransactionType_Name { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}