using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleUser : IIdentifiable 
    {
        ///<Summary>
        /// RoleUser IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// RoleUser ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// RoleUser Role.
        ///</Summary>
        public long RoleId {get; set;} = 0 ;
///<Summary>
        /// RoleUser User.
        ///</Summary>
        public long UserId {get; set;} = 0 ;

    }
}