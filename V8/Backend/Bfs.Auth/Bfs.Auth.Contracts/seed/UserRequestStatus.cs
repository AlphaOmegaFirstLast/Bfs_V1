using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class UserRequestStatus : IIdentifiable 
    {
        ///<Summary>
        /// UserRequestStatus IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// UserRequestStatus ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// UserRequestStatus Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// UserRequestStatus Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}