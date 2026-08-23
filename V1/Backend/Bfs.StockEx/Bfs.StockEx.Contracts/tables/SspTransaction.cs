using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class SspTransaction : IIdentifiable 
    {
        ///<Summary>
        /// SspTransaction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// SspTransaction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// SspTransaction Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// SspTransaction Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// SspTransaction Source Date.
        ///</Summary>
        public DateTime SourceDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// SspTransaction Transaction Date.
        ///</Summary>
        public DateTime TransactionDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// SspTransaction Source.
        ///</Summary>
        public string Source {get; set;} = string.Empty ;
///<Summary>
        /// SspTransaction Quantity.
        ///</Summary>
        public decimal Quantity {get; set;} = 0 ;
///<Summary>
        /// SspTransaction Price.
        ///</Summary>
        public decimal Price {get; set;} = 0 ;
///<Summary>
        /// SspTransaction To Quantity.
        ///</Summary>
        public decimal ToQuantity {get; set;} = 0 ;

        ///<Summary>
        /// SspTransaction StockShare Portfolio.
        ///</Summary>
        public long SsPortfolioId {get; set;} = 0 ;
///<Summary>
        /// SspTransaction Transaction Type.
        ///</Summary>
        public int TransactionTypeId {get; set;} = 0 ;
///<Summary>
        /// SspTransaction Stock Share.
        ///</Summary>
        public long StockShareId {get; set;} = 0 ;
///<Summary>
        /// SspTransaction To Portfolio.
        ///</Summary>
        public long ToPortfolioId {get; set;} = 0 ;

    }
}

