using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CreditInterest : IIdentifiable 
    {
        ///<Summary>
        /// CreditInterest ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// CreditInterest Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// CreditInterest Interest Percent.
        ///</Summary>
        public decimal InterestPercent {get; set;} = 0 ;
///<Summary>
        /// CreditInterest Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// CreditInterest IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;

        ///<Summary>
        /// CreditInterest Payment Mode.
        ///</Summary>
        public long PaymentModeId {get; set;} = 0 ;
///<Summary>
        /// CreditInterest Broker Agreement.
        ///</Summary>
        public long BrokerAgreementId {get; set;} = 0 ;

    }
}