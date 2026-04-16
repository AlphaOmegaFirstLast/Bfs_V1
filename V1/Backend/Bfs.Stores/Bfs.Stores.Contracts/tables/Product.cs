using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class Product : IIdentifiable 
    {
        ///<Summary>
        /// Product IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Product ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Product Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Product Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

