using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthRoleApp : IIdentifiable 
    {
        ///<Summary>
        /// AuthRoleApp IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// AuthRoleApp ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// AuthRoleApp Role.
        ///</Summary>
        public long AuthRoleId {get; set;} = 0 ;
///<Summary>
        /// AuthRoleApp System Application.
        ///</Summary>
        public long AuthAppId {get; set;} = 0 ;

    }
}