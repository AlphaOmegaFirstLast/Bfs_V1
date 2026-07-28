
namespace Bfs.Core.Services.Security
{
    public class TenantResourceRuleListItem : ITenantResourceRuleListItem
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long BfsComponentId { get; set; }

        public string RoleName { get; set; } = string.Empty;
        public string BfsComponentName { get; set; } = string.Empty;

        public string JoinStatement { get; set; } = string.Empty;
        public string WhereStatement { get; set; } = string.Empty;
        public string ParameterName { get; set; } = string.Empty;
        public string ParameterValue { get; set; } = string.Empty;
        public string ParameterType { get; set; } = string.Empty;
        public string SelectBlackList { get; set; } = string.Empty;
    }
}

