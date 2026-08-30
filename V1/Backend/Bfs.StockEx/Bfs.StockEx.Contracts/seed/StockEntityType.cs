using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class StockEntityType : IIdentifiable 
    {
        ///<Summary>
        /// StockEntityType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// StockEntityType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// StockEntityType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// StockEntityType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

