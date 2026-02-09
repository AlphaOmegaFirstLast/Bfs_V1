using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.BestFit.Data.Models
{
    public class ComponentSystemActionEntity : IIdentifiable, ITenanted
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;

        public long ComponentId {get; set;} = 0 ;
public int SystemActionId {get; set;} = 0 ;
public int ActionLocationId {get; set;} = 0 ;

    }
}