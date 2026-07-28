using Bfs.Core.Interfaces;

namespace Bfs.Core.Services.Security
{
    public class RoleResource
    {
        public long RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public long BfsComponentId { get; set; }
        public string BfsComponentName { get; set; } = string.Empty;

        // semi-colon separated fields names for black list. field names should be in the format of FieldName. for example: "Id", "AreaId", "Name"
        // if a component has a black list, then the user will not have access to the fields in the black list.
        public string SelectBlackList { get; set; } = string.Empty;
        public string JoinStatement { get; set; } = string.Empty;
        public string WhereStatement { get; set; } = string.Empty;
        public string ParameterName { get; set; } = string.Empty;
        public string ParameterType { get; set; } = string.Empty;
        public string ParameterValue { get; set; } = string.Empty;

        public static List<TenantResourceRuleListItem> GetRoleResource(IScopeData scopeData)
        {
            var list = new List<TenantResourceRuleListItem>();
            var rule = new TenantResourceRuleListItem
            {
                RoleId = 10,
                RoleName = "Role1",
                BfsComponentId = 15,
                BfsComponentName = "Store",
                JoinStatement = "Left Join strArea on strStore.AreaId = strArea.Id",
                WhereStatement = "strArea.Name = @AreaName",
                ParameterName = "@AreaName",
                ParameterType = "string",
                ParameterValue = "North Area"
            };
            list.Add(rule);
            rule = new TenantResourceRuleListItem
            {
                RoleId = 10,
                BfsComponentId = 15,
                RoleName = "Role1",
                BfsComponentName = "InvestorBroker",
                JoinStatement = "",
                WhereStatement = "stkInvestorBroker.InvestorId = @UserId",
                ParameterName = "@UserId",
                ParameterType = "long",
                ParameterValue = scopeData.UserId.ToString()
            };
            list.Add(rule);
            rule = new TenantResourceRuleListItem
            {
                RoleId = 10,
                RoleName = "Role1",
                BfsComponentId = 15,
                BfsComponentName = "Store",
                SelectBlackList = "Name",
                JoinStatement = "",
                WhereStatement = "",
                ParameterName = "",
                ParameterType = "",
                ParameterValue = ""
            };
            list.Add(rule);
            return list;
        }
    }
}
