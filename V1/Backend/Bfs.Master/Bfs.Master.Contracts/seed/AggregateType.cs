using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class AggregateType : IIdentifiable 
    {
        ///<Summary>
        /// AggregateType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// AggregateType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// AggregateType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// AggregateType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}