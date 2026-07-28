using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Auth.Data.Models
{
    public class ResourceRuleEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public string SelectBlackList {get; set;} = string.Empty ;
public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string BfsComponentName {get; set;} = string.Empty ;
public string JoinStatement {get; set;} = string.Empty ;
public string WhereStatement {get; set;} = string.Empty ;
public string ParameterName {get; set;} = string.Empty ;
public string ParameterValue {get; set;} = string.Empty ;
public string ParameterType {get; set;} = string.Empty ;
public string RoleName {get; set;} = string.Empty ;

        public long RoleId {get; set;} = 0 ;
public long BfsComponentId {get; set;} = 0 ;

    }
}

