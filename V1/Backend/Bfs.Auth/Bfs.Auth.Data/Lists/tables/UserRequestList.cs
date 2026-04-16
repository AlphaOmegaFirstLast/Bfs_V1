using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;

namespace Bfs.Auth.Data.Lists
{
    public class UserRequestList: QueryBase<UserRequestListFilter>,  IUserRequestList
    {
        public UserRequestList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<UserRequestListItem>> GetAsync(QueryRequest<UserRequestListFilter> request)
        {
            var response = new QueryResponse<UserRequestListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<UserRequestListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<UserRequestListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "athUserRequest.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUserRequest.AspNetUserId", QueryName = "AspNetUserId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUserRequest.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUserRequest.Name", QueryName = "Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUserRequest.Email", QueryName = "Email", IsAggregare = false });

            //lookups

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athUserRequest ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<UserRequestListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" athUserRequest.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine(" AND athUserRequest.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.AspNetUserId))
                {
                    sql.AppendLine("athUserRequest.AspNetUserId like '%'+@AspNetUserId+'%' ");
                    parameters.Add("@AspNetUserId", filter.AspNetUserId);
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("athUserRequest.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }
if (!string.IsNullOrEmpty(filter.Email))
                {
                    sql.AppendLine("athUserRequest.Email like '%'+@Email+'%' ");
                    parameters.Add("@Email", filter.Email);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<UserRequestListFilter> request, DynamicParameters parameters)
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

