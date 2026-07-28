namespace Bfs.Core.Services.Security
{
    public interface ITenantResourceRuleListFilter
    {
        long? BfsComponentId { get; set; }
        string? BfsComponentName { get; set; }
        long? Id { get; set; }
        string? JoinStatement { get; set; }
        string? ParameterName { get; set; }
        string? ParameterType { get; set; }
        string? ParameterValue { get; set; }
        long? RoleId { get; set; }
        string? RoleName { get; set; }
        string? SelectBlackList { get; set; }
        string? WhereStatement { get; set; }
    }
}