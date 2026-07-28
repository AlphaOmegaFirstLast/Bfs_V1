using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Stores.Data.Models
{
    public class DocumentEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public DateTime ResponseDate {get; set;} = DateTime.MinValue ;
public string Notes {get; set;} = string.Empty ;

        public long StoreId {get; set;} = 0 ;
public int OperationId {get; set;} = 0 ;

    }
}

