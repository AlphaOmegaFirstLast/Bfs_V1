using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleApp : IIdentifiable 
    {
        ///<Summary>
        /// RoleApp IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// RoleApp ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// RoleApp Role.
        ///</Summary>
        public long RoleId {get; set;} = 0 ;
///<Summary>
        /// RoleApp System Application.
        ///</Summary>
        public long AppId {get; set;} = 0 ;

    }
}