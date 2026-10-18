using Bfs.Core.Contracts;

namespace Bfs.StockEx.Contracts
{
    public class OverdraftRepCompareItem
    {
        public string? OverdraftPortfolio_Id { get; set; }
        public string? OverdraftPortfolio_OverdraftMax { get; set; }
        public string? OverdraftPortfolio_OverdraftPercent { get; set; }

        public string? OverdraftPortfolio_BrokerAgreementId { get; set; }
        public string? BrokerAgreement_BrokerId { get; set; }
        public string? BrokerAgreement_AgreementDate { get; set; }
        public string? BrokerAgreement_InvestorId { get; set; }
        public string? OverdraftPortfolio_Overdraft { get; set; }

        public string? SsPortfolioName { get; set; }
        public string? BrokerAgreementName { get; set; }
        public string? BrokerName { get; set; }
        public string? InvestorName { get; set; }
        public string? CurrencyName { get; set; }

        public string? sumTotalCashDeposit { get; set; }
        public string? SsPortfolioBalance_Balance { get; set; }
        public string? sumAvailableCredit { get; set; }
    }
}