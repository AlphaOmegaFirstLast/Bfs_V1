using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data;
using System.Text;

namespace Bfs.Master.Data.Lists
{
    public class BfsTenantSystemList: QueryBase<BfsTenantSystemListFilter>,  IBfsTenantSystemList
    {
        private readonly IResourceSecurity? _resourceSecurity;

        public BfsTenantSystemList(string connectionString, IResourceSecurity? resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsTenantSystemListItem>> GetAsync(QueryRequest<BfsTenantSystemListFilter> request)
        {
            var response = new QueryResponse<BfsTenantSystemListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsTenantSystemListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<BfsTenantSystemListItem> DoMapping(IEnumerable<BfsTenantSystemListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (BfsTenantSystemListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "BfsTenantSystem", FieldName = "Id", DbName = "BfsTenantSystem.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsTenantSystem", FieldName = "BfsTenantId", DbName = "BfsTenantSystem.BfsTenantId", QueryName = "BfsTenantId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsTenantSystem", FieldName = "BfsSystemId", DbName = "BfsTenantSystem.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "BfsTenant", FieldName = "Name", DbName = "BfsTenant.Name", QueryName = "BfsTenantName", IsAggregare = false});

            //autoCompletes
            _fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "Name", DbName = "BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false});

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
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("BfsTenantSystem.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

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

