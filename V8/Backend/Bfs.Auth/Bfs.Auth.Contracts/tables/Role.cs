using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class Role : IIdentifiable 
    {
        ///<Summary>
        /// Role IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Role ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Role Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Role Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}