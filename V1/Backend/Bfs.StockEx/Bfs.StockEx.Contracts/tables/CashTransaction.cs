using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CashTransaction : IIdentifiable 
    {
        ///<Summary>
        /// CashTransaction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// CashTransaction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// CashTransaction Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// CashTransaction Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// CashTransaction Source.
        ///</Summary>
        public string Source {get; set;} = string.Empty ;
///<Summary>
        /// CashTransaction Source Date.
        ///</Summary>
        public DateTime SourceDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// CashTransaction Transaction Date.
        ///</Summary>
        public DateTime TransactionDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// CashTransaction Value.
        ///</Summary>
        public decimal Value {get; set;} = 0 ;

        ///<Summary>
        /// CashTransaction StocksShare Transaction.
        ///</Summary>
        public long SspTransactionId {get; set;} = 0 ;
///<Summary>
        /// CashTransaction StockShare Portfolio.
        ///</Summary>
        public long SsPortfolioId {get; set;} = 0 ;
///<Summary>
        /// CashTransaction Transaction Type.
        ///</Summary>
        public int TransactionTypeId {get; set;} = 0 ;
///<Summary>
        /// CashTransaction Expenses Type.
        ///</Summary>
        public long ExpensesTypeId {get; set;} = 0 ;

    }
}

