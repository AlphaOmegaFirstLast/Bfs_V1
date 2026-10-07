using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Data.Lists
{
    public class UserRequestList: QueryBase<UserRequestListFilter>,  IUserRequestList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public UserRequestList(string connectionString, IResourceSecurity? resourceSecurity
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3
        )
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4
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
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<UserRequestListItem> DoMapping(IEnumerable<UserRequestListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (UserRequestListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "Id", DbName = "athUserRequest.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "Notes", DbName = "athUserRequest.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "Name", DbName = "athUserRequest.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "Email", DbName = "athUserRequest.Email", QueryName = "Email", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "UserId", DbName = "athUserRequest.UserId", QueryName = "UserId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "RequestDate", DbName = "athUserRequest.RequestDate", QueryName = "RequestDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "ResponseDate", DbName = "athUserRequest.ResponseDate", QueryName = "ResponseDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "UserRequestStatusId", DbName = "athUserRequest.UserRequestStatusId", QueryName = "UserRequestStatusId", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "UserRequestStatus", FieldName = "Name", DbName = "athUserRequestStatus.Name", QueryName = "UserRequestStatusName", IsAggregare = false});

            //autoCompletes

           //Aggregates

//Template_Start_Code_DontOverwrite_5
             _fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "Name", DbName = "athUserRequest.Name", QueryName = "AutoCompleteName", IsAggregare = false});
             _fieldList.Add(new QueryField() {ComponentName = "UserRequest", FieldName = "Name", DbName = "athUserRequest.Name", QueryName = "LookupName", IsAggregare = false});
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athUserRequest ");

           sql.AppendLine($"   Left Join athUserRequestStatus on athUserRequest.UserRequestStatusId = athUserRequestStatus.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

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
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athUserRequest.AspNetUserId like '%'+@AspNetUserId+'%' ");
                      parameters.Add("@AspNetUserId", filter.AspNetUserId);                    
                   }
                   else
                   {
                      sql.AppendLine("athUserRequest.AspNetUserId like '%'+@AspNetUserId+'%' ");
                      parameters.Add("@AspNetUserId", filter.AspNetUserId);
                   }
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athUserRequest.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);                    
                   }
                   else
                   {
                      sql.AppendLine("athUserRequest.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);
                   }
                }
if (!string.IsNullOrEmpty(filter.Email))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athUserRequest.Email like '%'+@Email+'%' ");
                      parameters.Add("@Email", filter.Email);                    
                   }
                   else
                   {
                      sql.AppendLine("athUserRequest.Email like '%'+@Email+'%' ");
                      parameters.Add("@Email", filter.Email);
                   }
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

                //Template_Start_Code_DontOverwrite_7
                //Template_End_Code_DontOverwrite_7      
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
//Template_Start_Code_DontOverwrite_8
//Template_End_Code_DontOverwrite_8       
    }
}