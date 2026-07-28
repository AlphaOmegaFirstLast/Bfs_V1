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
    public class UserRequestList: QueryBase<UserRequestListFilter>,  IUserRequestList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public UserRequestList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<UserRequestListItem>> GetAsync(QueryRequest<UserRequestListFilter> request)
        {
            var response = new QueryResponse<UserRequestListItem>();

            await SetUp(request, _resourceSecurity);

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
            _fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "Id", DbName = "athUserRequest.Id", QueryName = "UserRequest_Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "Notes", DbName = "athUserRequest.Notes", QueryName = "UserRequest_Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "Name", DbName = "athUserRequest.Name", QueryName = "UserRequest_Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "Email", DbName = "athUserRequest.Email", QueryName = "UserRequest_Email", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "UserId", DbName = "athUserRequest.UserId", QueryName = "UserRequest_UserId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "RequestDate", DbName = "athUserRequest.RequestDate", QueryName = "UserRequest_RequestDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "ResponseDate", DbName = "athUserRequest.ResponseDate", QueryName = "UserRequest_ResponseDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "UserRequestStatusId", DbName = "athUserRequest.UserRequestStatusId", QueryName = "UserRequest_UserRequestStatusId", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "UserRequestStatus", FieldName = "Name", DbName = "athUserRequestStatus.Name", QueryName = "UserRequestStatusName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athUserRequest ");

           sql.AppendLine($"   Left Join athUserRequestStatus on athUserRequest.UserRequestStatusId = athUserRequestStatus.Id");

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
                    sql.AppendLine("athUserRequest.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }
if ((filter.UserId.HasValue) && (filter.UserId>0))
                {
                    sql.AppendLine("athUserRequest.UserId = @UserId");
                    parameters.Add("@UserId", filter.UserId);
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

                if (filter.UserRequestStatusId.HasValue)
                {
                    sql.AppendLine("athUserRequest.UserRequestStatusId = @UserRequestStatusId");
                    parameters.Add("@UserRequestStatusId", filter.UserRequestStatusId.Value);
                }

                if (filter.RequestDate?.From.HasValue == true)
                {
                    sql.AppendLine("athUserRequest.RequestDate >= @RequestDateFrom");
                    parameters.Add("@RequestDateFrom", filter.RequestDate.From.Value);
                }
                if (filter.RequestDate?.To.HasValue == true)
                {
                    sql.AppendLine("athUserRequest.RequestDate <= @RequestDateTo");
                    parameters.Add("@RequestDateTo", filter.RequestDate.To.Value);
                }
if (filter.ResponseDate?.From.HasValue == true)
                {
                    sql.AppendLine("athUserRequest.ResponseDate >= @ResponseDateFrom");
                    parameters.Add("@ResponseDateFrom", filter.ResponseDate.From.Value);
                }
                if (filter.ResponseDate?.To.HasValue == true)
                {
                    sql.AppendLine("athUserRequest.ResponseDate <= @ResponseDateTo");
                    parameters.Add("@ResponseDateTo", filter.ResponseDate.To.Value);
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

