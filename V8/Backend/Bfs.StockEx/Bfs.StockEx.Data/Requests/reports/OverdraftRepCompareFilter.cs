using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class OverdraftRepCompareFilter
    {
        public long? OverdraftPortfolio_Id { get; set; }
    
        public long? OverdraftPortfolio_BrokerAgreementId { get; set; }
        public long? BrokerAgreement_BrokerId { get; set; }
        public long? BrokerAgreement_InvestorId { get; set; }
        public DateRange? BrokerAgreement_AgreementDate { get; set; }
        public NumericRange? OverdraftPortfolio_OverdraftMax { get; set; }
        public NumericRange? OverdraftPortfolio_OverdraftPercent { get; set; }
        public NumericRange? OverdraftPortfolio_Overdraft { get; set; }

        public NumericRange? sumTotalCashDeposit { get; set; }
        public NumericRange? sumAvailableCredit { get; set; }
        public NumericRange? SsPortfolioBalance_Balance { get; set; }
      
       

    }
}