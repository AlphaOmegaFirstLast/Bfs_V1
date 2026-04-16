using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;

namespace Bfs.Auth.Data.Lists
{
    public class RoleAppList: QueryBase<RoleAppListFilter>,  IRoleAppList
    {
        public RoleAppList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<RoleAppListItem>> GetAsync(QueryRequest<RoleAppListFilter> request)
        {
            var response = new QueryResponse<RoleAppListItem>();

            SetUp(request);

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
            _fieldList.Add(new QueryField() { DbName = "athRoleApp.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athRoleApp.RoleId", QueryName = "RoleId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athRoleApp.AppId", QueryName = "AppId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "athRole.Name", QueryName = "RoleName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athApp.Name", QueryName = "AppName", IsAggregare = false });

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
                    sql.AppendLine(" AND athRoleApp.Id = @Id");
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

