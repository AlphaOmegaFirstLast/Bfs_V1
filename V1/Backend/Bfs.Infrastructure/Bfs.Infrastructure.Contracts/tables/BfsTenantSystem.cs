using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsTenantSystem : IIdentifiable 
    {
        ///<Summary>
        /// BfsTenantSystem IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BfsTenantSystem ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// BfsTenantSystem Tenant Name.
        ///</Summary>
        public long BfsTenantId {get; set;} = 0 ;
///<Summary>
        /// BfsTenantSystem BestFit System.
        ///</Summary>
        public long BfsSystemId {get; set;} = 0 ;

    }
}