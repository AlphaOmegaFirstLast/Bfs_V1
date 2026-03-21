using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthRole : IIdentifiable 
    {
        ///<Summary>
        /// AuthRole IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// AuthRole ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// AuthRole Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// AuthRole Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}