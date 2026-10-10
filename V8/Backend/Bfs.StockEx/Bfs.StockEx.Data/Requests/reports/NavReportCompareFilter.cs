using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class NavReportCompareFilter
    {

        public string? SsPortfolio_Name { get; set; }

        public long? SsPortfolio_InvestorId { get; set; }

        public long? SsPortfolio_BrokerId { get; set; }

        public NumericRange? sumValue { get; set; }
public NumericRange? sumBalance { get; set; }
public NumericRange? sumNav { get; set; }

    }
}