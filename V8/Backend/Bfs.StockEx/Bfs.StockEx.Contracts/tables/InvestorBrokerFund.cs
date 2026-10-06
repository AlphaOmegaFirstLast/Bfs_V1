using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class InvestorBrokerFund : IIdentifiable 
    {
        ///<Summary>
        /// InvestorBrokerFund IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// InvestorBrokerFund ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// InvestorBrokerFund Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// InvestorBrokerFund Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// InvestorBrokerFund Fund.
        ///</Summary>
        public decimal Fund {get; set;} = 0 ;
///<Summary>
        /// InvestorBrokerFund Fund Date.
        ///</Summary>
        public DateTime FundDate {get; set;} = DateTime.MinValue ;

        ///<Summary>
        /// InvestorBrokerFund Broker.
        ///</Summary>
        public long BrokerId {get; set;} = 0 ;
///<Summary>
        /// InvestorBrokerFund Investor.
        ///</Summary>
        public long InvestorId {get; set;} = 0 ;

    }
}