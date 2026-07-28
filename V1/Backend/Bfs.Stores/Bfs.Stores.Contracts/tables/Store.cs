using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class Store : IIdentifiable 
    {
        ///<Summary>
        /// Store IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Store ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Store Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Store Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// Store Area.
        ///</Summary>
        public long AreaId {get; set;} = 0 ;

    }
}

