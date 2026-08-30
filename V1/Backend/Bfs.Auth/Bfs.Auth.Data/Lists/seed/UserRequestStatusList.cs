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
    public class UserRequestStatusList: QueryBase<UserRequestStatusListFilter>,  IUserRequestStatusList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public UserRequestStatusList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity ?? throw new ArgumentNullException(nameof(resourceSecurity));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<UserRequestStatusListItem>> GetAsync(QueryRequest<UserRequestStatusListFilter> request)
        {
            var response = new QueryResponse<UserRequestStatusListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<UserRequestStatusListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<UserRequestStatusListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "UserRequestStatus", FieldName = "Id", DbName = "athUserRequestStatus.Id", QueryName = "UserRequestStatus_Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequestStatus", FieldName = "Name", DbName = "athUserRequestStatus.Name", QueryName = "UserRequestStatus_Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequestStatus", FieldName = "Notes", DbName = "athUserRequestStatus.Notes", QueryName = "UserRequestStatus_Notes", IsAggregare = false});

            //lookups

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athUserRequestStatus ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<UserRequestStatusListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" athUserRequestStatus.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("athUserRequestStatus.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("athUserRequestStatus.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<UserRequestStatusListFilter> request, DynamicParameters parameters)
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

