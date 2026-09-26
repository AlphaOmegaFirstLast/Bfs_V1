using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class FilterType : IIdentifiable 
    {
        ///<Summary>
        /// FilterType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// FilterType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// FilterType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// FilterType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}