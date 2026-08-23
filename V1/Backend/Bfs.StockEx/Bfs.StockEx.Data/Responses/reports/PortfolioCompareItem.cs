using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class PortfolioCompareItem
    {
        public string? SsPortfolio_Name { get; set; }
public string? Broker_Name { get; set; }
public string? Investor_Name { get; set; }
public string? SspTransaction_Quantity { get; set; }
public string? SspTransaction_Price { get; set; }
public string? SspTransaction_TransactionDate { get; set; }
public string? StockShare_Name { get; set; }
public string? TransactionType_Name { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}

