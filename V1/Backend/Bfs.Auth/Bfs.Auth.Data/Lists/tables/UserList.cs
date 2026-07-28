using Bfs.Core.Data;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;

namespace Bfs.Auth.Data.Lists
{
    public class UserList: QueryBase<UserListFilter>,  IUserList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public UserList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<UserListItem>> GetAsync(QueryRequest<UserListFilter> request)
        {
            var response = new QueryResponse<UserListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<UserListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<UserListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "Id", DbName = "athUser.Id", QueryName = "User_Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "AspNetUserId", DbName = "athUser.AspNetUserId", QueryName = "User_AspNetUserId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "Notes", DbName = "athUser.Notes", QueryName = "User_Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "Name", DbName = "athUser.Name", QueryName = "User_Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "Email", DbName = "athUser.Email", QueryName = "User_Email", IsAggregare = false});

            //lookups

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athUser ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<UserListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" athUser.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("athUser.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.AspNetUserId))
                {
                    sql.AppendLine("athUser.AspNetUserId like '%'+@AspNetUserId+'%' ");
                    parameters.Add("@AspNetUserId", filter.AspNetUserId);
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("athUser.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }
if (!string.IsNullOrEmpty(filter.Email))
                {
                    sql.AppendLine("athUser.Email like '%'+@Email+'%' ");
                    parameters.Add("@Email", filter.Email);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<UserListFilter> request, DynamicParameters parameters)
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

