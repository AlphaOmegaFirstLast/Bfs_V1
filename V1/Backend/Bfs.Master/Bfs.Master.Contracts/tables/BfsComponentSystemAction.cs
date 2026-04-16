using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsComponentSystemAction : IIdentifiable 
    {
        ///<Summary>
        /// BfsComponentSystemAction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BfsComponentSystemAction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// BfsComponentSystemAction Component Name.
        ///</Summary>
        public long BfsComponentId {get; set;} = 0 ;
///<Summary>
        /// BfsComponentSystemAction System Action.
        ///</Summary>
        public long SystemActionId {get; set;} = 0 ;
///<Summary>
        /// BfsComponentSystemAction Action Location.
        ///</Summary>
        public int ActionLocationId {get; set;} = 0 ;

    }
}