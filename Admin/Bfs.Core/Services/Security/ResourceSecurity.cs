using Bfs.Core.Data;
using Bfs.Core.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static System.Net.WebRequestMethods;

namespace Bfs.Core.Services.Security
{
    public class ResourceSecurity: IResourceSecurity
    {
        private readonly IScopeData _scopeData;
        private readonly ITenantResourceRuleList _tenantResourceRuleList;
        private List<TenantResourceRuleListItem>? _roleResourceList;

        public ResourceSecurity(IScopeData scopeData, ITenantResourceRuleList tenantResourceRuleList)
        {
            _scopeData = scopeData;
            var ruleFilter = new QueryRequest<TenantResourceRuleListFilter> { Filter = new TenantResourceRuleListFilter() };
            _tenantResourceRuleList = tenantResourceRuleList;
           // _roleResourceList = RoleResource.GetRoleResource(scopeData);
        }

        public async Task SetRoleResourceAsync()
        {
            if (_roleResourceList == null)
            {
                var ruleFilter = new QueryRequest<TenantResourceRuleListFilter> { Filter = new TenantResourceRuleListFilter() };
                // Todo Consider caching per tenant, or background refresh to avoid performance lock.    
                _roleResourceList = (await _tenantResourceRuleList.GetAsync(ruleFilter)).Items;
            }
        }

        public bool ApplySecuritySelect(QueryField queryField)
        {
            var inputRoleId = _scopeData.RoleId;
            var isBlackList = false;
            foreach (var rule in _roleResourceList)
            {
                if (rule.RoleId == inputRoleId && queryField.ComponentName == rule.BfsComponentName)  
                {
                    isBlackList = rule.SelectBlackList != null && rule.SelectBlackList.Split(',').Contains(queryField.FieldName, StringComparer.OrdinalIgnoreCase);
                    if (isBlackList)
                        break;
                }
            }

            return !isBlackList;
        }

        public string AddSecurityJoin(string queryJoinStatment)
        {
            var inputRoleId = _scopeData.RoleId;
            StringBuilder joinStatement = new StringBuilder(queryJoinStatment + " ");
            foreach (var rule in _roleResourceList)
            {
                //Todo: improve the logic to identify if [SystemPrefix][ComponentName] already exists in the query to avoid appending duplicate join statements.
                //Currently it is doing a simple string contains which may not be sufficient in complex queries.
                if (rule.RoleId == inputRoleId && queryJoinStatment.ToLower().Contains(rule.BfsComponentName.ToLower()))  //RoleName can also be used here if RoleId is not unique
                {
                    if (!joinStatement.ToString().ToLower().Contains(rule.JoinStatement.ToLower()))
                        joinStatement.AppendLine(rule.JoinStatement);
                }
            }

            return joinStatement.ToString();
        }

        public string AddSecurityWhere(string queryJoinStatment, string queryWhereStatment)
        {
            var inputRoleId = _scopeData.RoleId;
            StringBuilder whereStatement = new StringBuilder(queryWhereStatment + " ");
            foreach (var rule in _roleResourceList)
            {
                if (rule.RoleId == inputRoleId && queryJoinStatment.ToLower().Contains(rule.BfsComponentName.ToLower()))  //RoleName can also be used here if RoleId is not unique
                {
                    if (!whereStatement.ToString().ToLower().Contains(rule.WhereStatement.ToLower()))
                        whereStatement.AppendLine(" And " + rule.WhereStatement);
                }
            }

            return whereStatement.ToString();
        }

        // The user will have access to Only Allowed Records
        // Records Retrieved by the query will be filtered based on the parameters added in this method. So only records that match the parameter values defined in the rules for the role will be retrieved.
        public DynamicParameters AddSecurityParameter(string queryWhereStatment, DynamicParameters parameters)
        {
            var inputRoleId = _scopeData.RoleId;
            foreach (var rule in _roleResourceList)
            {
                //Todo if the ParameterName exists then handle case or raise exception.
                if (rule.RoleId == inputRoleId && queryWhereStatment.Contains(rule.ParameterName))  //RoleName can also be used here if RoleId is not unique
                {
                    switch (rule.ParameterType)
                    {
                        case "string":
                            parameters.Add(rule.ParameterName, (string)rule.ParameterValue);
                            break;
                        case "long":
                            parameters.Add(rule.ParameterName, long.Parse(rule.ParameterValue));
                            break;
                        case "int":
                            parameters.Add(rule.ParameterName, int.Parse(rule.ParameterValue));
                            break;
                            // Add more cases as needed for other types
                    }
                }
            }

            return parameters;
        }

        // This method can be used before executing Crud operation to check if the user has access to the specific record based on the rules defined for the component.
        public bool IsApplicableToCRUD<T>(string componentName, T entity) where T : class
        {
            var inputRoleId = _scopeData.RoleId;
            var ok = true;
            foreach (var rule in _roleResourceList)
            {
                if (rule.RoleId == inputRoleId && rule.BfsComponentName == componentName)
                {
                    var property = entity.GetType().GetProperty(rule.ParameterName.TrimStart('@'));
                    var propertyValue = property?.GetValue(entity);
                    var expectedValue = Convert.ChangeType(rule.ParameterValue, Type.GetType("System." + rule.ParameterType, true));
                    ok = propertyValue != null && propertyValue.Equals(expectedValue);
                    if (!ok)
                        break;
                }
            }
            return ok;
        }

        public bool IsApplicableToQuery(List<QueryField> fieldList)
        {
            var inputRoleId = _scopeData.RoleId;
            var ok = _roleResourceList.Any(rule => rule.RoleId == inputRoleId && fieldList.Any(f => f.ComponentName.ToLower() == rule.BfsComponentName.ToLower())); ;
            return ok;
        }
    }
}
