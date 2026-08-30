using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class SourceType : IIdentifiable 
    {
        ///<Summary>
        /// SourceType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// SourceType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// SourceType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// SourceType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

