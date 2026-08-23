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
    public class AppList: QueryBase<AppListFilter>,  IAppList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public AppList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<AppListItem>> GetAsync(QueryRequest<AppListFilter> request)
        {
            var response = new QueryResponse<AppListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<AppListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<AppListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "App", FieldName = "Id", DbName = "athApp.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "App", FieldName = "Name", DbName = "athApp.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "App", FieldName = "Notes", DbName = "athApp.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "App", FieldName = "BfsSystemId", DbName = "athApp.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "App", FieldName = "Logo", DbName = "athApp.Logo", QueryName = "Logo", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "Name", DbName = "BestFit_V6.dbo.BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athApp ");

           sql.AppendLine($"   Left Join BestFit_V6.dbo.BfsSystem on athApp.BfsSystemId = BestFit_V6.dbo.BfsSystem.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<AppListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" athApp.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("athApp.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("athApp.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }
if (!string.IsNullOrEmpty(filter.Logo))
                {
                    sql.AppendLine("athApp.Logo like '%'+@Logo+'%' ");
                    parameters.Add("@Logo", filter.Logo);
                }

                if (filter.BfsSystemId.HasValue)
                {
                    sql.AppendLine("athApp.BfsSystemId = @BfsSystemId");
                    parameters.Add("@BfsSystemId", filter.BfsSystemId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<AppListFilter> request, DynamicParameters parameters)
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

