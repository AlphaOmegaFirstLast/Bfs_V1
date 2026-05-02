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
_fieldList.Add(new QueryField() { DbName = "athUserRequest.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUserRequest.Name", QueryName = "Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUserRequest.Email", QueryName = "Email", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUserRequest.UserId", QueryName = "UserId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUserRequest.RequestDate", QueryName = "RequestDate", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUserRequest.ResponseDate", QueryName = "ResponseDate", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athUserRequest.UserRequestStatusId", QueryName = "UserRequestStatusId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "athUserRequestStatus.Name", QueryName = "UserRequestStatusName", IsAggregare = false });

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

