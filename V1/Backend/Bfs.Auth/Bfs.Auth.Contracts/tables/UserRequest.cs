using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;
using Bfs.Core.Contracts.Auth;

namespace Bfs.Auth.Contracts
{
    public class UserRequest : IIdentifiable ,IAspnetUserRequest
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
///<Summary>
        /// UserRequest User ID.
        ///</Summary>
        public long UserId {get; set;} = 0 ;
///<Summary>
        /// UserRequest Request Date.
        ///</Summary>
        public DateTime RequestDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// UserRequest Response Date.
        ///</Summary>
        public DateTime ResponseDate {get; set;} = DateTime.MinValue ;

        ///<Summary>
        /// UserRequest User Request Status.
        ///</Summary>
        public long UserRequestStatusId {get; set;} = 0 ;

    }
}

