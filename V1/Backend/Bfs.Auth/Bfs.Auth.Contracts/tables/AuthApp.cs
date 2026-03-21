using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthApp : IIdentifiable 
    {
        ///<Summary>
        /// AuthApp IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// AuthApp ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// AuthApp Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// AuthApp Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// AuthApp BestFit System.
        ///</Summary>
        public long BfsSystemId {get; set;} = 0 ;

    }
}