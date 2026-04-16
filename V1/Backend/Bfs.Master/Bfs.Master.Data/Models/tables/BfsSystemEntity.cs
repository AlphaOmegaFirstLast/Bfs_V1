using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Master.Data.Models
{
    public class BfsSystemEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public bool IsMaster {get; set;} = false ;
public string Notes {get; set;} = string.Empty ;
public string BasePortNumber {get; set;} = string.Empty ;
public string DbPrefix {get; set;} = string.Empty ;
public string Logo {get; set;} = string.Empty ;
public string Name {get; set;} = string.Empty ;

        public int SystemTemplateId {get; set;} = 0 ;

    }
}