using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class BfsTenantSystemList: QueryBase<BfsTenantSystemListFilter>,  IBfsTenantSystemList
    {
        public BfsTenantSystemList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsTenantSystemListItem>> GetAsync(QueryRequest<BfsTenantSystemListFilter> request)
        {
            var response = new QueryResponse<BfsTenantSystemListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsTenantSystemListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BfsTenantSystemListItem>)items;

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = db.ExecuteScalar<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() { DbName = "BfsTenantSystem.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsTenantSystem.BfsTenantId", QueryName = "BfsTenantId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsTenantSystem.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "BfsTenant.Name", QueryName = "BfsTenantName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsTenantSystem ");

           sql.AppendLine($"   Left Join BfsTenant on BfsTenantSystem.BfsTenantId = BfsTenant.Id");
sql.AppendLine($"   Left Join BfsSystem on BfsTenantSystem.BfsSystemId = BfsSystem.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsTenantSystemListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsTenantSystem.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (filter.BfsTenantId.HasValue)
                {
                    sql.AppendLine("BfsTenantSystem.BfsTenantId = @BfsTenantId");
                    parameters.Add("@BfsTenantId", filter.BfsTenantId.Value);
                }
if (filter.BfsSystemId.HasValue)
                {
                    sql.AppendLine("BfsTenantSystem.BfsSystemId = @BfsSystemId");
                    parameters.Add("@BfsSystemId", filter.BfsSystemId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsTenantSystemListFilter> request, DynamicParameters parameters)
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