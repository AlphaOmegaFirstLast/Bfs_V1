using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class SsPortfolioBalance : IIdentifiable 
    {
        ///<Summary>
        /// SsPortfolioBalance IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// SsPortfolioBalance ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// SsPortfolioBalance Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// SsPortfolioBalance Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// SsPortfolioBalance Balance.
        ///</Summary>
        public decimal Balance {get; set;} = 0 ;

        ///<Summary>
        /// SsPortfolioBalance  Portfolio.
        ///</Summary>
        public long SsPortfolioId {get; set;} = 0 ;
///<Summary>
        /// SsPortfolioBalance Currency.
        ///</Summary>
        public long CurrencyId {get; set;} = 0 ;

    }
}

