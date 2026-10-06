using Bfs.Core.Data;

namespace Bfs.StockEx.Data
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