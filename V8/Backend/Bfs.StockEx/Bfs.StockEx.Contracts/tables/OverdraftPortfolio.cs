using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class OverdraftPortfolio : IIdentifiable 
    {
        ///<Summary>
        /// OverdraftPortfolio IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// OverdraftPortfolio ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// OverdraftPortfolio Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// OverdraftPortfolio Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// OverdraftPortfolio Overdraft Percent.
        ///</Summary>
        public decimal OverdraftPercent {get; set;} = 0 ;
///<Summary>
        /// OverdraftPortfolio Overdraft Max.
        ///</Summary>
        public decimal OverdraftMax {get; set;} = 0 ;

        ///<Summary>
        /// OverdraftPortfolio StockShare Portfolio.
        ///</Summary>
        public long SsPortfolioId {get; set;} = 0 ;
///<Summary>
        /// OverdraftPortfolio Broker Agreement.
        ///</Summary>
        public long BrokerAgreementId {get; set;} = 0 ;

    }
}