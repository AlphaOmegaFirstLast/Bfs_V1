using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class ResourceRuleListItem
    {      
        public string? ResourceRule_SelectBlackList { get; set; }
public string? ResourceRule_Id { get; set; }
public string? ResourceRule_RoleId { get; set; }
public string? ResourceRule_BfsComponentName { get; set; }
public string? ResourceRule_JoinStatement { get; set; }
public string? ResourceRule_WhereStatement { get; set; }
public string? ResourceRule_ParameterName { get; set; }
public string? ResourceRule_ParameterValue { get; set; }
public string? ResourceRule_ParameterType { get; set; }
public string? ResourceRule_BfsComponentId { get; set; }
public string? ResourceRule_RoleName { get; set; }

        public string? RoleName { get; set; }
public string? BfsComponentName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

