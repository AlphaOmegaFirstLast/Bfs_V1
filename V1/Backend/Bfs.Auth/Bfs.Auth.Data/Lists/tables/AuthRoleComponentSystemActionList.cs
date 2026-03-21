using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;

namespace Bfs.Auth.Data.Lists
{
    public class AuthRoleComponentSystemActionList: QueryBase<AuthRoleComponentSystemActionListFilter>,  IAuthRoleComponentSystemActionList
    {
        public AuthRoleComponentSystemActionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<AuthRoleComponentSystemActionListItem>> GetAsync(QueryRequest<AuthRoleComponentSystemActionListFilter> request)
        {
            var response = new QueryResponse<AuthRoleComponentSystemActionListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<AuthRoleComponentSystemActionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<AuthRoleComponentSystemActionListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "AuthRoleComponentSystemAction.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthRoleComponentSystemAction.BfsComponentId", QueryName = "BfsComponentId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthRoleComponentSystemAction.SystemActionId", QueryName = "SystemActionId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthRoleComponentSystemAction.AuthRoleId", QueryName = "AuthRoleId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.Name", QueryName = "SystemActionName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthRole.Name", QueryName = "AuthRoleName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From AuthRoleComponentSystemAction ");

           sql.AppendLine($"   Left Join [BestFit_V4].[dbo].BfsComponent on AuthRoleComponentSystemAction.BfsComponentId = BfsComponent.Id");
sql.AppendLine($"   Left Join [BestFit_V4].[dbo].SystemAction on AuthRoleComponentSystemAction.SystemActionId = SystemAction.Id");
sql.AppendLine($"   Left Join AuthRole on AuthRoleComponentSystemAction.AuthRoleId = AuthRole.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<AuthRoleComponentSystemActionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" AuthRoleComponentSystemAction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (filter.BfsComponentId.HasValue)
                {
                    sql.AppendLine("AuthRoleComponentSystemAction.BfsComponentId = @BfsComponentId");
                    parameters.Add("@BfsComponentId", filter.BfsComponentId.Value);
                }
if (filter.SystemActionId.HasValue)
                {
                    sql.AppendLine("AuthRoleComponentSystemAction.SystemActionId = @SystemActionId");
                    parameters.Add("@SystemActionId", filter.SystemActionId.Value);
                }
if (filter.AuthRoleId.HasValue)
                {
                    sql.AppendLine("AuthRoleComponentSystemAction.AuthRoleId = @AuthRoleId");
                    parameters.Add("@AuthRoleId", filter.AuthRoleId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<AuthRoleComponentSystemActionListFilter> request, DynamicParameters parameters)
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