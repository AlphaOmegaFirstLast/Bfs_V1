using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleComponentSystemAction : IIdentifiable 
    {
        ///<Summary>
        /// RoleComponentSystemAction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// RoleComponentSystemAction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// RoleComponentSystemAction Component Name.
        ///</Summary>
        public long BfsComponentId {get; set;} = 0 ;
///<Summary>
        /// RoleComponentSystemAction System Action.
        ///</Summary>
        public long SystemActionId {get; set;} = 0 ;
///<Summary>
        /// RoleComponentSystemAction Role.
        ///</Summary>
        public long RoleId {get; set;} = 0 ;

    }
}