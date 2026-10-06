using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class ExpensesType : IIdentifiable 
    {
        ///<Summary>
        /// ExpensesType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// ExpensesType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// ExpensesType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// ExpensesType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}