using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Infrastructure.Data.Models
{
    public class SystemActionEntity : IIdentifiable, ITenanted
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public string MatchProperty {get; set;} = string.Empty ;
public string MatchValues {get; set;} = string.Empty ;
public string ActionTemplate {get; set;} = string.Empty ;

        public int ActionTypeId {get; set;} = 0 ;
public int WriterTypeId {get; set;} = 0 ;

    }
}