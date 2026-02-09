using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Infrastructure.Data.Models
{
    public class BfsComponentEntity : IIdentifiable, ITenanted
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public bool IsSoftDelete {get; set;} = false ;
public string Name {get; set;} = string.Empty ;
public string DisplayName {get; set;} = string.Empty ;
public string MenuName {get; set;} = string.Empty ;
public string MenuPlaceHolder {get; set;} = string.Empty ;
public string QueryBaseTable {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;

        public long BfsSystemId {get; set;} = 0 ;
public int DataTypeId {get; set;} = 0 ;

    }
}