using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class User : IIdentifiable ,IAuthUser
    {
        ///<Summary>
        /// User IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// User ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// User AspNetUserId.
        ///</Summary>
        public string AspNetUserId {get; set;} = string.Empty ;
///<Summary>
        /// User Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// User Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// User Email.
        ///</Summary>
        public string Email {get; set;} = string.Empty ;

    }
}