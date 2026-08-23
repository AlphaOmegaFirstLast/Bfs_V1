using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.StockEx.Data.Models
{
    public class SspStockEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public decimal Quantity {get; set;} = 0 ;
public decimal AverageCost {get; set;} = 0 ;

        public long SsPortfolioId {get; set;} = 0 ;
public long StockShareId {get; set;} = 0 ;

    }
}