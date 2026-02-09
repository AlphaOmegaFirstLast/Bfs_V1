using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Infrastructure.Data.Models
{
    public class BfsComponentBusinessActionEntity : IIdentifiable, ITenanted
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;

        public long BfsComponentId {get; set;} = 0 ;
public long BusinessActionId {get; set;} = 0 ;
public int ActionLocationId {get; set;} = 0 ;

    }
}