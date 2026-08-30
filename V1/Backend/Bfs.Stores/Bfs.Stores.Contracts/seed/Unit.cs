using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class Unit : IIdentifiable 
    {
        ///<Summary>
        /// Unit IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Unit ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Unit Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Unit Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

