using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;

namespace Bfs.Auth.Data.Lists
{
    public class RoleUserList: QueryBase<RoleUserListFilter>,  IRoleUserList
    {
        public RoleUserList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<RoleUserListItem>> GetAsync(QueryRequest<RoleUserListFilter> request)
        {
            var response = new QueryResponse<RoleUserListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<RoleUserListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<RoleUserListItem>)items;

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() { DbName = "athRoleUser.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athRoleUser.RoleId", QueryName = "RoleId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athRoleUser.UserId", QueryName = "UserId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "athRole.Name", QueryName = "RoleName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUser.Name", QueryName = "UserName", IsAggregare = false });

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athRoleUser ");

           sql.AppendLine($"   Left Join athRole on athRoleUser.RoleId = athRole.Id");
sql.AppendLine($"   Left Join athUser on athRoleUser.UserId = athUser.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<RoleUserListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" athRoleUser.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("athRoleUser.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (filter.RoleId.HasValue)
                {
                    sql.AppendLine("athRoleUser.RoleId = @RoleId");
                    parameters.Add("@RoleId", filter.RoleId.Value);
                }
if (filter.UserId.HasValue)
                {
                    sql.AppendLine("athRoleUser.UserId = @UserId");
                    parameters.Add("@UserId", filter.UserId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<RoleUserListFilter> request, DynamicParameters parameters)
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

