using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class Transaction : IIdentifiable 
    {
        ///<Summary>
        /// Transaction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Transaction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Transaction Quantity.
        ///</Summary>
        public decimal Quantity {get; set;} = 0 ;
///<Summary>
        /// Transaction Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// Transaction Store.
        ///</Summary>
        public long StoreId {get; set;} = 0 ;
///<Summary>
        /// Transaction Operation.
        ///</Summary>
        public int OperationId {get; set;} = 0 ;
///<Summary>
        /// Transaction Product.
        ///</Summary>
        public long ProductId {get; set;} = 0 ;

    }
}

