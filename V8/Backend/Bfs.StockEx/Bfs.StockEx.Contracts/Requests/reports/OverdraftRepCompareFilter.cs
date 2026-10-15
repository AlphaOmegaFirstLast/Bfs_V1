using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class OverdraftRepCompareFilter
    {

        public long? OverdraftPortfolio_SsPortfolioId { get; set; }
public long? BrokerAgreement_InvestorId { get; set; }
public long? BrokerAgreement_BrokerId { get; set; }
public long? SsPortfolioBalance_CurrencyId { get; set; }

    }
}