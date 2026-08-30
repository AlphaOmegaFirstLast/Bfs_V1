using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class Currency : IIdentifiable 
    {
        ///<Summary>
        /// Currency IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Currency ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Currency Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Currency Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

