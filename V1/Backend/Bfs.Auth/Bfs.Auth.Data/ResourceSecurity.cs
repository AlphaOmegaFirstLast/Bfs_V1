using Bfs.Core.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Bfs.Auth.Data
{
    public class ResourceSecurity : IResourceSecurity
    {
        private readonly IScopeData _scopeData;
        public List<RoleResource> RoleResourceList { get; set; } = new List<RoleResource>();
        public ResourceSecurity(IScopeData scopeData)
        {
            _scopeData = scopeData;
            RoleResourceList = RoleResource.GetRoleResource();
        }

        public string AddSecurityJoin(string queryJoinStatment)
        {
            var inputRoleId = _scopeData.RoleId;
            StringBuilder joinStatement = new StringBuilder(queryJoinStatment + " ");
            foreach (var rule in RoleResourceList)
            {
                if (rule.RoleId == inputRoleId && queryJoinStatment.Contains(rule.ComponentName))  //RoleName can also be used here if RoleId is not unique
                {
                    joinStatement.AppendLine(rule.JoinStatement);
                }
            }

            return joinStatement.ToString();
        }

        public string AddSecurityWhere(string queryJoinStatment, string queryWhereStatment)
        {
            var inputRoleId = _scopeData.RoleId;
            StringBuilder whereStatement = new StringBuilder(queryWhereStatment + " ");
            foreach (var rule in RoleResourceList)
            {
                if (rule.RoleId == inputRoleId && queryJoinStatment.Contains(rule.ComponentName))  //RoleName can also be used here if RoleId is not unique
                {
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
            foreach (var rule in RoleResourceList)
            {
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

        public void Apply(ref string queryJoinStatment, ref string queryWhereStatment, ref DynamicParameters parameters)
        {
            if (string.IsNullOrEmpty(queryJoinStatment))
            {
                queryJoinStatment = string.Empty;
            }
            if (string.IsNullOrEmpty(queryWhereStatment))
            {
                queryWhereStatment = string.Empty;
            }
            if (parameters == null)
            {
                parameters = new DynamicParameters();
            }
            var join = AddSecurityJoin(queryJoinStatment);
            var where = AddSecurityWhere(join, queryWhereStatment);
            parameters = AddSecurityParameter(where, parameters);
        }

        // This method can be used before executing Crud operation to check if the user has access to the specific record based on the rules defined for the component.
        public bool CheckSecurity<T>(string componentName, T entity) where T : class
        {
            var inputRoleId = _scopeData.RoleId;
            var ok = true;
            foreach (var rule in RoleResourceList)
            {
                if (rule.RoleId == inputRoleId && rule.ComponentName == componentName)
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
    }

    public class RoleResource
    {
        public long RoleId { get; set; }
        public long BfsComponentId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string ComponentName { get; set; } = string.Empty;

        public string JoinStatement { get; set; } = string.Empty;
        public string WhereStatement { get; set; } = string.Empty;
        public string ParameterName { get; set; } = string.Empty;
        public string ParameterType { get; set; } = string.Empty;
        public string ParameterValue { get; set; } = string.Empty;

        public static List<RoleResource> GetRoleResource()
        {
            var list = new List<RoleResource>();
            var rule = new RoleResource
            {
                RoleId = 10,
                RoleName = "Role1",
                BfsComponentId = 15,
                ComponentName = "strStore",
                JoinStatement = "Left join strArea on strArea.Id = strStore.AreaId",
                WhereStatement = "strArea.Name = @AreaName",
                ParameterName = "@AreaName",
                ParameterType = "string",
                ParameterValue = "North"
            };
            list.Add(rule);
            rule = new RoleResource
            {
                RoleId = 10,
                BfsComponentId = 15,
                RoleName = "Role1",
                ComponentName = "stkInvestorBroker",
                JoinStatement = "",
                WhereStatement = "stkInvestorBroker.InvestorId = @UserId",
                ParameterName = "@UserId",
                ParameterType = "long",
                ParameterValue = "12345"
            };
            list.Add(rule);
            return list;
        }
    }
}
