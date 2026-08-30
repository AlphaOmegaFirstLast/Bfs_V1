using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class StockFieldType : IIdentifiable 
    {
        ///<Summary>
        /// StockFieldType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// StockFieldType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// StockFieldType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// StockFieldType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

