using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class TransferCostType : IIdentifiable 
    {
        ///<Summary>
        /// TransferCostType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// TransferCostType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// TransferCostType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// TransferCostType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

