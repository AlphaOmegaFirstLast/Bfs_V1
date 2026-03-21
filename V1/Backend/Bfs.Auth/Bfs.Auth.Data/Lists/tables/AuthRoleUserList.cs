using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;

namespace Bfs.Auth.Data.Lists
{
    public class AuthRoleUserList: QueryBase<AuthRoleUserListFilter>,  IAuthRoleUserList
    {
        public AuthRoleUserList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<AuthRoleUserListItem>> GetAsync(QueryRequest<AuthRoleUserListFilter> request)
        {
            var response = new QueryResponse<AuthRoleUserListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<AuthRoleUserListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<AuthRoleUserListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "AuthRoleUser.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthRoleUser.AuthRoleId", QueryName = "AuthRoleId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthRoleUser.AuthUserId", QueryName = "AuthUserId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "AuthRole.Name", QueryName = "AuthRoleName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthUser.Name", QueryName = "AuthUserName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From AuthRoleUser ");

           sql.AppendLine($"   Left Join AuthRole on AuthRoleUser.AuthRoleId = AuthRole.Id");
sql.AppendLine($"   Left Join AuthUser on AuthRoleUser.AuthUserId = AuthUser.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<AuthRoleUserListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" AuthRoleUser.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (filter.AuthRoleId.HasValue)
                {
                    sql.AppendLine("AuthRoleUser.AuthRoleId = @AuthRoleId");
                    parameters.Add("@AuthRoleId", filter.AuthRoleId.Value);
                }
if (filter.AuthUserId.HasValue)
                {
                    sql.AppendLine("AuthRoleUser.AuthUserId = @AuthUserId");
                    parameters.Add("@AuthUserId", filter.AuthUserId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<AuthRoleUserListFilter> request, DynamicParameters parameters)
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