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
    public class UserList: QueryBase<UserListFilter>,  IUserList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public UserList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<UserListItem>> GetAsync(QueryRequest<UserListFilter> request)
        {
            var response = new QueryResponse<UserListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<UserListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<UserListItem> DoMapping(IEnumerable<UserListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (UserListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "Id", DbName = "athUser.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "AspNetUserId", DbName = "athUser.AspNetUserId", QueryName = "AspNetUserId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "Notes", DbName = "athUser.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "Name", DbName = "athUser.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "Email", DbName = "athUser.Email", QueryName = "Email", IsAggregare = false});

            //object fields

            //lookups

            //autoCompletes

           //Aggregates

//Template_Start_Code_DontOverwrite_5
             _fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "Name", DbName = "athUser.Name", QueryName = "AutoCompleteName", IsAggregare = false});
             _fieldList.Add(new QueryField() {ComponentName = "User", FieldName = "Name", DbName = "athUser.Name", QueryName = "LookupName", IsAggregare = false});
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athUser ");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

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
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athUser.AspNetUserId like '%'+@AspNetUserId+'%' ");
                      parameters.Add("@AspNetUserId", filter.AspNetUserId);                    
                   }
                   else
                   {
                      sql.AppendLine("athUser.AspNetUserId like '%'+@AspNetUserId+'%' ");
                      parameters.Add("@AspNetUserId", filter.AspNetUserId);
                   }
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athUser.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);                    
                   }
                   else
                   {
                      sql.AppendLine("athUser.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);
                   }
                }
if (!string.IsNullOrEmpty(filter.Email))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athUser.Email like '%'+@Email+'%' ");
                      parameters.Add("@Email", filter.Email);                    
                   }
                   else
                   {
                      sql.AppendLine("athUser.Email like '%'+@Email+'%' ");
                      parameters.Add("@Email", filter.Email);
                   }
                }

                //Template_Start_Code_DontOverwrite_7
                //Template_End_Code_DontOverwrite_7      
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
//Template_Start_Code_DontOverwrite_8
//Template_End_Code_DontOverwrite_8       
    }
}