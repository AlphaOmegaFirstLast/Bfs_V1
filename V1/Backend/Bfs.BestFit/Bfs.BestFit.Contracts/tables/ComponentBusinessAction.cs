using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class ComponentBusinessAction : IIdentifiable
    {
        ///<Summary>
        /// ComponentBusinessAction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// ComponentBusinessAction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// ComponentBusinessAction Component Name.
        ///</Summary>
        public long ComponentId {get; set;} = 0 ;
///<Summary>
        /// ComponentBusinessAction Business Action.
        ///</Summary>
        public long BusinessActionId {get; set;} = 0 ;
///<Summary>
        /// ComponentBusinessAction Menu Action.
        ///</Summary>
        public int ActionLocationId {get; set;} = 0 ;

    }
}