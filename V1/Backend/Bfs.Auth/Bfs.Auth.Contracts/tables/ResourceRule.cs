using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class ResourceRule : IIdentifiable 
    {
        ///<Summary>
        /// ResourceRule Select Statement  BlackList fields.
        ///</Summary>
        public string SelectBlackList {get; set;} = string.Empty ;
///<Summary>
        /// ResourceRule IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// ResourceRule ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// ResourceRule BfsComponent Name.
        ///</Summary>
        public string BfsComponentName {get; set;} = string.Empty ;
///<Summary>
        /// ResourceRule Join Statement.
        ///</Summary>
        public string JoinStatement {get; set;} = string.Empty ;
///<Summary>
        /// ResourceRule Where Statement.
        ///</Summary>
        public string WhereStatement {get; set;} = string.Empty ;
///<Summary>
        /// ResourceRule Parameter Name.
        ///</Summary>
        public string ParameterName {get; set;} = string.Empty ;
///<Summary>
        /// ResourceRule Parameter Value.
        ///</Summary>
        public string ParameterValue {get; set;} = string.Empty ;
///<Summary>
        /// ResourceRule Parameter Type.
        ///</Summary>
        public string ParameterType {get; set;} = string.Empty ;
///<Summary>
        /// ResourceRule Role Name.
        ///</Summary>
        public string RoleName {get; set;} = string.Empty ;

        ///<Summary>
        /// ResourceRule Role.
        ///</Summary>
        public long RoleId {get; set;} = 0 ;
///<Summary>
        /// ResourceRule BfsComponent.
        ///</Summary>
        public long BfsComponentId {get; set;} = 0 ;

    }
}

