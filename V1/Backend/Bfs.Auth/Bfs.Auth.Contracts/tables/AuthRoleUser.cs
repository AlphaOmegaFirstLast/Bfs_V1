using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthRoleUser : IIdentifiable 
    {
        ///<Summary>
        /// AuthRoleUser IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// AuthRoleUser ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// AuthRoleUser Role.
        ///</Summary>
        public long AuthRoleId {get; set;} = 0 ;
///<Summary>
        /// AuthRoleUser Users.
        ///</Summary>
        public long AuthUserId {get; set;} = 0 ;

    }
}