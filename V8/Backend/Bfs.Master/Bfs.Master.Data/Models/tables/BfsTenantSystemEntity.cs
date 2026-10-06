using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Master.Data.Models
{
    public class BfsTenantSystemEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;

        public long BfsTenantId {get; set;} = 0 ;
public long BfsSystemId {get; set;} = 0 ;

    }
}