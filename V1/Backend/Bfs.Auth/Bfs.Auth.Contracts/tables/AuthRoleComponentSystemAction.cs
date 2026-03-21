using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthRoleComponentSystemAction : IIdentifiable 
    {
        ///<Summary>
        /// AuthRoleComponentSystemAction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// AuthRoleComponentSystemAction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// AuthRoleComponentSystemAction Component Name.
        ///</Summary>
        public long BfsComponentId {get; set;} = 0 ;
///<Summary>
        /// AuthRoleComponentSystemAction System Action.
        ///</Summary>
        public long SystemActionId {get; set;} = 0 ;
///<Summary>
        /// AuthRoleComponentSystemAction Role.
        ///</Summary>
        public long AuthRoleId {get; set;} = 0 ;

    }
}