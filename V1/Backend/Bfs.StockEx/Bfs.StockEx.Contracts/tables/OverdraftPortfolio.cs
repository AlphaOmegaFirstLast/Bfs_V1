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
        /// OverdraftPortfolio Overdraft Value.
        ///</Summary>
        public decimal OverdraftValue {get; set;} = 0 ;

        ///<Summary>
        /// OverdraftPortfolio StockShare Portfolio.
        ///</Summary>
        public long SsPortfolioId {get; set;} = 0 ;

    }
}