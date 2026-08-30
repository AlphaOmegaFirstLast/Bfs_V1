using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class Area : IIdentifiable 
    {
        ///<Summary>
        /// Area IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Area ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Area Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Area Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}