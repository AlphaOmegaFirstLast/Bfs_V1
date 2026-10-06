using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class NavReportCompareItem
    {
        public string? SsPortfolio_Name { get; set; }
public string? Currency_Name { get; set; }

        public string? sumPrice { get; set; }
public string? sumBalance { get; set; }
public string? sumNav { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}