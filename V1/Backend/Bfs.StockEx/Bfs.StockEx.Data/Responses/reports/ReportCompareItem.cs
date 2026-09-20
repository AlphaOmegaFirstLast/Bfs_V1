using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class ReportCompareItem
    {
        public string? SsPortfolio_Name { get; set; }
public string? Currency_Name { get; set; }

        public string? sumQuantity { get; set; }
public string? sumPrice { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}