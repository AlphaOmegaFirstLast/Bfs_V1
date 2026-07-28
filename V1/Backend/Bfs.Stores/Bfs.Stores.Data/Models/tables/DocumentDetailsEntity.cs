using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Stores.Data.Models
{
    public class DocumentDetailsEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public decimal Quantity {get; set;} = 0 ;
public string Notes {get; set;} = string.Empty ;

        public long ProductId {get; set;} = 0 ;
public int UnitId {get; set;} = 0 ;
public long DocumentId {get; set;} = 0 ;

    }
}

