using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.StockEx.Data.Models
{
    public class CashTransactionEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public string Source {get; set;} = string.Empty ;
public DateTime SourceDate {get; set;} = DateTime.MinValue ;
public DateTime TransactionDate {get; set;} = DateTime.MinValue ;
public decimal Value {get; set;} = 0 ;

        public long SspTransactionId {get; set;} = 0 ;
public long SsPortfolioId {get; set;} = 0 ;
public int TransactionTypeId {get; set;} = 0 ;
public long ExpensesTypeId {get; set;} = 0 ;
public long CurrencyId {get; set;} = 0 ;

    }
}

