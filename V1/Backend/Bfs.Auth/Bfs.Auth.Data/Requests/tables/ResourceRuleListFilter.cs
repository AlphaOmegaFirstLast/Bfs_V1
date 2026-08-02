using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class ResourceRuleListFilter
    {
        public long? Id { get; set; }

        public string? SelectBlackList { get; set; }
public string? BfsComponentName { get; set; }
public string? JoinStatement { get; set; }
public string? WhereStatement { get; set; }
public string? ParameterName { get; set; }
public string? ParameterValue { get; set; }
public string? ParameterType { get; set; }
public string? RoleName { get; set; }

        public long? RoleId { get; set; }
public long? BfsComponentId { get; set; }

    }
}