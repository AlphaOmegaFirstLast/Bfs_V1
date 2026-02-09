using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class ComponentSystemAction : IIdentifiable
    {
        ///<Summary>
        /// ComponentSystemAction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// ComponentSystemAction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// ComponentSystemAction Component Name.
        ///</Summary>
        public long ComponentId {get; set;} = 0 ;
///<Summary>
        /// ComponentSystemAction Menu Action.
        ///</Summary>
        public int SystemActionId {get; set;} = 0 ;
///<Summary>
        /// ComponentSystemAction Menu Action.
        ///</Summary>
        public int ActionLocationId {get; set;} = 0 ;

    }
}