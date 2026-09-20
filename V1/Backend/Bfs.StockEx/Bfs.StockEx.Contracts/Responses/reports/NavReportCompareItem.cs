using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class NavReportCompareItem
    {
        public string? SsPortfolio_Name { get; set; }
public string? Currency_Name { get; set; }

        public string? StockValue { get; set; }
public string? Cash { get; set; }
        public string? NAV { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}

