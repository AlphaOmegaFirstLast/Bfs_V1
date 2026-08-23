using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.StockEx.Data.Models
{
    public class SspTransactionEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public DateTime SourceDate {get; set;} = DateTime.MinValue ;
public DateTime TransactionDate {get; set;} = DateTime.MinValue ;
public string Source {get; set;} = string.Empty ;
public decimal Quantity {get; set;} = 0 ;
public decimal Price {get; set;} = 0 ;
public decimal ToQuantity {get; set;} = 0 ;

        public long SsPortfolioId {get; set;} = 0 ;
public int TransactionTypeId {get; set;} = 0 ;
public long StockShareId {get; set;} = 0 ;
public long ToPortfolioId {get; set;} = 0 ;

    }
}

