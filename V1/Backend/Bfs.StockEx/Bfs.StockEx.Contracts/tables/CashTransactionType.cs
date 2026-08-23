using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CashTransactionType : IIdentifiable 
    {
        ///<Summary>
        /// CashTransactionType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// CashTransactionType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// CashTransactionType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// CashTransactionType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// CashTransactionType Effect.
        ///</Summary>
        public int EffectId {get; set;} = 0 ;

    }
}