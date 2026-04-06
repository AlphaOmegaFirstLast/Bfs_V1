using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;

namespace Bfs.Auth.Data.Lists
{
    public class RoleComponentSystemActionList: QueryBase<RoleComponentSystemActionListFilter>,  IRoleComponentSystemActionList
    {
        public RoleComponentSystemActionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<RoleComponentSystemActionListItem>> GetAsync(QueryRequest<RoleComponentSystemActionListFilter> request)
        {
            var response = new QueryResponse<RoleComponentSystemActionListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<RoleComponentSystemActionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<RoleComponentSystemActionListItem>)items;

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = db.ExecuteScalar<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() { DbName = "athRoleComponentSystemAction.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athRoleComponentSystemAction.BfsComponentId", QueryName = "BfsComponentId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athRoleComponentSystemAction.SystemActionId", QueryName = "SystemActionId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athRoleComponentSystemAction.RoleId", QueryName = "RoleId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "athBfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athSystemAction.Name", QueryName = "SystemActionName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athRole.Name", QueryName = "RoleName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From [DbParentTable] ");

           sql.AppendLine($"   Left Join BfsComponent on athRoleComponentSystemAction.BfsComponentId = BfsComponent.Id");
sql.AppendLine($"   Left Join SystemAction on athRoleComponentSystemAction.SystemActionId = SystemAction.Id");
sql.AppendLine($"   Left Join Role on athRoleComponentSystemAction.RoleId = Role.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<RoleComponentSystemActionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" [DbParentTable].isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (filter.BfsComponentId.HasValue)
                {
                    sql.AppendLine("athRoleComponentSystemAction.BfsComponentId = @BfsComponentId");
                    parameters.Add("@BfsComponentId", filter.BfsComponentId.Value);
                }
if (filter.SystemActionId.HasValue)
                {
                    sql.AppendLine("athRoleComponentSystemAction.SystemActionId = @SystemActionId");
                    parameters.Add("@SystemActionId", filter.SystemActionId.Value);
                }
if (filter.RoleId.HasValue)
                {
                    sql.AppendLine("athRoleComponentSystemAction.RoleId = @RoleId");
                    parameters.Add("@RoleId", filter.RoleId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<RoleComponentSystemActionListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
       }       
    }
}

