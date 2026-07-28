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
    public class RoleAppList: QueryBase<RoleAppListFilter>,  IRoleAppList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public RoleAppList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<RoleAppListItem>> GetAsync(QueryRequest<RoleAppListFilter> request)
        {
            var response = new QueryResponse<RoleAppListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<RoleAppListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<RoleAppListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "RoleApp", FieldName = "Id", DbName = "athRoleApp.Id", QueryName = "RoleApp_Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "RoleApp", FieldName = "RoleId", DbName = "athRoleApp.RoleId", QueryName = "RoleApp_RoleId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "RoleApp", FieldName = "AppId", DbName = "athRoleApp.AppId", QueryName = "RoleApp_AppId", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "Role", FieldName = "Name", DbName = "athRole.Name", QueryName = "RoleName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "App", FieldName = "Name", DbName = "athApp.Name", QueryName = "AppName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athRoleApp ");

           sql.AppendLine($"   Left Join athRole on athRoleApp.RoleId = athRole.Id");
sql.AppendLine($"   Left Join athApp on athRoleApp.AppId = athApp.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<RoleAppListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" athRoleApp.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("athRoleApp.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (filter.RoleId.HasValue)
                {
                    sql.AppendLine("athRoleApp.RoleId = @RoleId");
                    parameters.Add("@RoleId", filter.RoleId.Value);
                }
if (filter.AppId.HasValue)
                {
                    sql.AppendLine("athRoleApp.AppId = @AppId");
                    parameters.Add("@AppId", filter.AppId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<RoleAppListFilter> request, DynamicParameters parameters)
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

