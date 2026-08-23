using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class BrokerAgreement : IIdentifiable 
    {
        ///<Summary>
        /// BrokerAgreement Agreement Date.
        ///</Summary>
        public DateTime AgreementDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// BrokerAgreement IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BrokerAgreement ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// BrokerAgreement Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// BrokerAgreement Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// BrokerAgreement Overdraft Percent.
        ///</Summary>
        public decimal OverdraftPrcnt {get; set;} = 0 ;
///<Summary>
        /// BrokerAgreement Overdraft Max.
        ///</Summary>
        public decimal OverdraftMx {get; set;} = 0 ;

        ///<Summary>
        /// BrokerAgreement Investor.
        ///</Summary>
        public long InvestorId {get; set;} = 0 ;
///<Summary>
        /// BrokerAgreement Broker.
        ///</Summary>
        public long BrokerId {get; set;} = 0 ;
///<Summary>
        /// BrokerAgreement StockShare Portfolio.
        ///</Summary>
        public long SsPortfolioId {get; set;} = 0 ;

    }
}