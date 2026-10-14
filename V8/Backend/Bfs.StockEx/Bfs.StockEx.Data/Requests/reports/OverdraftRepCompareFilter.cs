using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class OverdraftRepCompareFilter
    {
        public long? OverdraftPortfolio_Id { get; set; }

        public string? OverdraftPortfolio_Name { get; set; }

        public long? OverdraftPortfolio_SsPortfolioId { get; set; }
public long? OverdraftPortfolio_BrokerAgreementId { get; set; }

        public NumericRange? OverdraftPortfolio_OverdraftPercent { get; set; }
public NumericRange? OverdraftPortfolio_OverdraftMax { get; set; }

    }
}