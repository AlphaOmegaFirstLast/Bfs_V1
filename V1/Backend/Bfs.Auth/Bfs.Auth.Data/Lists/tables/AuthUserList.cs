using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;

namespace Bfs.Auth.Data.Lists
{
    public class AuthUserList: QueryBase<AuthUserListFilter>,  IAuthUserList
    {
        public AuthUserList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<AuthUserListItem>> GetAsync(QueryRequest<AuthUserListFilter> request)
        {
            var response = new QueryResponse<AuthUserListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<AuthUserListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<AuthUserListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "AuthUser.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthUser.AspNetUserId", QueryName = "AspNetUserId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthUser.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "AuthUser.Name", QueryName = "Name", IsAggregare = false });

            //lookups

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From AuthUser ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<AuthUserListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" AuthUser.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.AspNetUserId))
                {
                    sql.AppendLine("AuthUser.AspNetUserId like '%'+@AspNetUserId+'%' ");
                    parameters.Add("@AspNetUserId", filter.AspNetUserId);
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("AuthUser.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<AuthUserListFilter> request, DynamicParameters parameters)
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
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

