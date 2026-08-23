using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CurrentPrice : IIdentifiable 
    {
        ///<Summary>
        /// CurrentPrice IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// CurrentPrice ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// CurrentPrice Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// CurrentPrice Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// CurrentPrice Transaction Date.
        ///</Summary>
        public DateTime TransactionDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// CurrentPrice Price.
        ///</Summary>
        public decimal Price {get; set;} = 0 ;

        ///<Summary>
        /// CurrentPrice Stock Share.
        ///</Summary>
        public long StockShareId {get; set;} = 0 ;

    }
}