using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class ActionType : IIdentifiable 
    {
        ///<Summary>
        /// ActionType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// ActionType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// ActionType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// ActionType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}