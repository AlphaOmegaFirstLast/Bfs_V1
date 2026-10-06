using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class PortfolioCashTransactionAggregateCompareItem
    {
        public string? SsPortfolio_Name { get; set; }
public string? Broker_Name { get; set; }
public string? Investor_Name { get; set; }

        public string? sumValue { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}