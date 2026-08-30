using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class TransactionType : IIdentifiable 
    {
        ///<Summary>
        /// TransactionType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// TransactionType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// TransactionType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// TransactionType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// TransactionType Effect Type.
        ///</Summary>
        public int EffectTypeId {get; set;} = 0 ;
///<Summary>
        /// TransactionType Applicable To Entity.
        ///</Summary>
        public int StockEntityTypeId {get; set;} = 0 ;
///<Summary>
        /// TransactionType Calculation Method.
        ///</Summary>
        public int CalculationMethodId {get; set;} = 0 ;
///<Summary>
        /// TransactionType Source Type.
        ///</Summary>
        public int SourceTypeId {get; set;} = 0 ;
///<Summary>
        /// TransactionType Applicable To Field.
        ///</Summary>
        public int StockFieldTypeId {get; set;} = 0 ;
///<Summary>
        /// TransactionType Next Transaction Type.
        ///</Summary>
        public int NextTransactionTypeId {get; set;} = 0 ;

    }
}

