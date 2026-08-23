using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class SspStock : IIdentifiable 
    {
        ///<Summary>
        /// SspStock IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// SspStock ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// SspStock Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// SspStock Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// SspStock Quantity.
        ///</Summary>
        public decimal Quantity {get; set;} = 0 ;
///<Summary>
        /// SspStock Average Cost.
        ///</Summary>
        public decimal AverageCost {get; set;} = 0 ;

        ///<Summary>
        /// SspStock StockShare Portfolio.
        ///</Summary>
        public long SsPortfolioId {get; set;} = 0 ;
///<Summary>
        /// SspStock StockShare .
        ///</Summary>
        public long StockShareId {get; set;} = 0 ;

    }
}