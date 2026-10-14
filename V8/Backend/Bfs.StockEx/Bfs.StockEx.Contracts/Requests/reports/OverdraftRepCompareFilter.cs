using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class OverdraftRepCompareFilter
    {
        public long? OverdraftPortfolio_Id { get; set; }

        public long? BrokerAgreement_SsPortfolioId { get; set; }
public long? OverdraftPortfolio_BrokerAgreementId { get; set; }
public long? BrokerAgreement_BrokerId { get; set; }
public long? BrokerAgreement_InvestorId { get; set; }

        public NumericRange? OverdraftPortfolio_OverdraftMax { get; set; }
public NumericRange? OverdraftPortfolio_Overdraft { get; set; }

        public NumericRange? sumTotalCashDeposit { get; set; }
public NumericRange? sumAvailableCredit { get; set; }

    }
}