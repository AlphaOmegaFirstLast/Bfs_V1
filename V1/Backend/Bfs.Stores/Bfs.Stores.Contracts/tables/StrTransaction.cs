using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class StrTransaction : IIdentifiable 
    {
        ///<Summary>
        /// StrTransaction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// StrTransaction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// StrTransaction Quantity.
        ///</Summary>
        public decimal Quantity {get; set;} = 0 ;
///<Summary>
        /// StrTransaction Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// StrTransaction Store.
        ///</Summary>
        public long StrStoreId {get; set;} = 0 ;
///<Summary>
        /// StrTransaction Operation.
        ///</Summary>
        public int StrOperationId {get; set;} = 0 ;
///<Summary>
        /// StrTransaction Product.
        ///</Summary>
        public long StrProductId {get; set;} = 0 ;

    }
}