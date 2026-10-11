using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class ResourceRuleListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public string SelectBlackList { get; set; }
public long Id { get; set; }
public long RoleId { get; set; }
public string BfsComponentName { get; set; }
public string JoinStatement { get; set; }
public string WhereStatement { get; set; }
public string ParameterName { get; set; }
public string ParameterValue { get; set; }
public string ParameterType { get; set; }
public long BfsComponentId { get; set; }
public string RoleName { get; set; }



   }
}