using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class Document : IIdentifiable 
    {
        ///<Summary>
        /// Document IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Document ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Document Doc No..
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Document Response Date.
        ///</Summary>
        public DateTime ResponseDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// Document Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// Document Store.
        ///</Summary>
        public long StoreId {get; set;} = 0 ;
///<Summary>
        /// Document Operation.
        ///</Summary>
        public int OperationId {get; set;} = 0 ;

    }
}

