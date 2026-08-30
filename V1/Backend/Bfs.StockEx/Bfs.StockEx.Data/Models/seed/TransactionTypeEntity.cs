using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.StockEx.Data.Models
{
    public class TransactionTypeEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;

        public int EffectTypeId {get; set;} = 0 ;
public int StockEntityTypeId {get; set;} = 0 ;
public int CalculationMethodId {get; set;} = 0 ;
public int SourceTypeId {get; set;} = 0 ;
public int StockFieldTypeId {get; set;} = 0 ;
public int NextTransactionTypeId {get; set;} = 0 ;

    }
}

