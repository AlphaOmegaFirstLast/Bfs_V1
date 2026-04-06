using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class UserRequest : IIdentifiable 
    {
        ///<Summary>
        /// UserRequest IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// UserRequest ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// UserRequest AspNetUserId.
        ///</Summary>
        public string AspNetUserId {get; set;} = string.Empty ;
///<Summary>
        /// UserRequest Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// UserRequest Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// UserRequest Email.
        ///</Summary>
        public string Email {get; set;} = string.Empty ;

    }
}

