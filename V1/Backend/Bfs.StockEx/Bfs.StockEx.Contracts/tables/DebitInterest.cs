using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class DebitInterest : IIdentifiable 
    {
        ///<Summary>
        /// DebitInterest ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// DebitInterest Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// DebitInterest Interest Percent.
        ///</Summary>
        public decimal InterestPercent {get; set;} = 0 ;
///<Summary>
        /// DebitInterest Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// DebitInterest IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;

        ///<Summary>
        /// DebitInterest Payment Mode.
        ///</Summary>
        public long PaymentModeId {get; set;} = 0 ;
///<Summary>
        /// DebitInterest Broker Agreement.
        ///</Summary>
        public long BrokerAgreementId {get; set;} = 0 ;

    }
}