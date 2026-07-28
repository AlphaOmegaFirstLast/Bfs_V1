using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class DocumentDetails : IIdentifiable 
    {
        ///<Summary>
        /// DocumentDetails IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// DocumentDetails ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// DocumentDetails Quantity.
        ///</Summary>
        public decimal Quantity {get; set;} = 0 ;
///<Summary>
        /// DocumentDetails Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// DocumentDetails Product.
        ///</Summary>
        public long ProductId {get; set;} = 0 ;
///<Summary>
        /// DocumentDetails Unit.
        ///</Summary>
        public int UnitId {get; set;} = 0 ;
///<Summary>
        /// DocumentDetails Document.
        ///</Summary>
        public long DocumentId {get; set;} = 0 ;

    }
}

