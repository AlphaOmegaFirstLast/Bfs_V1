using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsComponentBusinessAction : IIdentifiable
    {
        ///<Summary>
        /// BfsComponentBusinessAction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BfsComponentBusinessAction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// BfsComponentBusinessAction Component Name.
        ///</Summary>
        public long BfsComponentId {get; set;} = 0 ;
///<Summary>
        /// BfsComponentBusinessAction Business Action.
        ///</Summary>
        public long BusinessActionId {get; set;} = 0 ;
///<Summary>
        /// BfsComponentBusinessAction Menu Action.
        ///</Summary>
        public int ActionLocationId {get; set;} = 0 ;

    }
}