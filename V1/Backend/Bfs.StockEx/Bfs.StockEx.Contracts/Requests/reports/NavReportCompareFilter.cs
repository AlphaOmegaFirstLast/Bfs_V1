using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class NavReportCompareFilter
    {

        public string? SsPortfolio_Name { get; set; }
        public string? Currency_Name { get; set; }
        public NumericRange? StockValue { get; set; }
public NumericRange? Cash { get; set; }
        public NumericRange? NAV { get; set; }
    }
}

