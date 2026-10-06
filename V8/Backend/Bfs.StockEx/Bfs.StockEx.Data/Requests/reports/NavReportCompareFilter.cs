using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class NavReportCompareFilter
    {

        public string? SsPortfolio_Name { get; set; }

        public NumericRange? sumPrice { get; set; }
public NumericRange? sumBalance { get; set; }
public NumericRange? sumNav { get; set; }

    }
}