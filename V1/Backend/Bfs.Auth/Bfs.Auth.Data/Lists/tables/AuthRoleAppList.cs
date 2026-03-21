using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;

namespace Bfs.Auth.Data.Lists
{
    public class AuthRoleAppList: QueryBase<AuthRoleAppListFilter>,  IAuthRoleAppList
    {
        public AuthRoleAppList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<AuthRoleAppListItem>> GetAsync(QueryRequest<AuthRoleAppListFilter> request)
        {
            var response = new QueryResponse<AuthRoleAppListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<AuthRoleAppListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<AuthRoleAppListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "AuthRoleApp.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthRoleApp.AuthRoleId", QueryName = "AuthRoleId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthRoleApp.AuthAppId", QueryName = "AuthAppId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "AuthRole.Name", QueryName = "AuthRoleName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthApp.Name", QueryName = "AuthAppName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From AuthRoleApp ");

           sql.AppendLine($"   Left Join AuthRole on AuthRoleApp.AuthRoleId = AuthRole.Id");
sql.AppendLine($"   Left Join AuthApp on AuthRoleApp.AuthAppId = AuthApp.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<AuthRoleAppListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" AuthRoleApp.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (filter.AuthRoleId.HasValue)
                {
                    sql.AppendLine("AuthRoleApp.AuthRoleId = @AuthRoleId");
                    parameters.Add("@AuthRoleId", filter.AuthRoleId.Value);
                }
if (filter.AuthAppId.HasValue)
                {
                    sql.AppendLine("AuthRoleApp.AuthAppId = @AuthAppId");
                    parameters.Add("@AuthAppId", filter.AuthAppId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<AuthRoleAppListFilter> request, DynamicParameters parameters)
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