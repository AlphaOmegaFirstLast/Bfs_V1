using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class OverdraftRepCompareItem
    {
        public string? OverdraftPortfolio_Id { get; set; }
public string? OverdraftPortfolio_OverdraftMax { get; set; }
public string? BrokerAgreement_SsPortfolioId { get; set; }
public string? OverdraftPortfolio_BrokerAgreementId { get; set; }
public string? BrokerAgreement_BrokerId { get; set; }
public string? BrokerAgreement_InvestorId { get; set; }
public string? OverdraftPortfolio_Overdraft { get; set; }

        public string? SsPortfolioName { get; set; }
public string? BrokerAgreementName { get; set; }
public string? BrokerName { get; set; }
public string? InvestorName { get; set; }

        public string? sumTotalCashDeposit { get; set; }
public string? sumAvailableCredit { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}