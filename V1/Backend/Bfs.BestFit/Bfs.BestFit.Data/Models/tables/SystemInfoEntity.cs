using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.BestFit.Data.Models
{
    public class SystemInfoEntity : IIdentifiable, ITenanted
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public string BasePortNumber {get; set;} = string.Empty ;

        public long ClientId {get; set;} = 0 ;
public int SystemTemplateId {get; set;} = 0 ;

    }
}