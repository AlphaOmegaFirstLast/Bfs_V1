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
    public class BfsManualCodeList: QueryBase<BfsManualCodeListFilter>,  IBfsManualCodeList
    {
        private readonly IResourceSecurity? _resourceSecurity;

        public BfsManualCodeList(string connectionString, IResourceSecurity? resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsManualCodeListItem>> GetAsync(QueryRequest<BfsManualCodeListFilter> request)
        {
            var response = new QueryResponse<BfsManualCodeListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsManualCodeListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<BfsManualCodeListItem> DoMapping(IEnumerable<BfsManualCodeListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (BfsManualCodeListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "BfsManualCode", FieldName = "Id", DbName = "BfsManualCode.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsManualCode", FieldName = "BfsSystemId", DbName = "BfsManualCode.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsManualCode", FieldName = "BfsComponentId", DbName = "BfsManualCode.BfsComponentId", QueryName = "BfsComponentId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsManualCode", FieldName = "Name", DbName = "BfsManualCode.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsManualCode", FieldName = "FileName", DbName = "BfsManualCode.FileName", QueryName = "FileName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsManualCode", FieldName = "StartTemplate", DbName = "BfsManualCode.StartTemplate", QueryName = "StartTemplate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsManualCode", FieldName = "EndTemplate", DbName = "BfsManualCode.EndTemplate", QueryName = "EndTemplate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsManualCode", FieldName = "Code", DbName = "BfsManualCode.Code", QueryName = "Code", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsManualCode", FieldName = "Notes", DbName = "BfsManualCode.Notes", QueryName = "Notes", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "Name", DbName = "BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "Name", DbName = "BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsManualCode ");

           sql.AppendLine($"   Left Join BfsSystem on BfsManualCode.BfsSystemId = BfsSystem.Id");
sql.AppendLine($"   Left Join BfsComponent on BfsManualCode.BfsComponentId = BfsComponent.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsManualCodeListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsManualCode.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("BfsManualCode.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("BfsManualCode.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }
if (!string.IsNullOrEmpty(filter.FileName))
                {
                    sql.AppendLine("BfsManualCode.FileName like '%'+@FileName+'%' ");
                    parameters.Add("@FileName", filter.FileName);
                }

                if (filter.BfsSystemId.HasValue)
                {
                    sql.AppendLine("BfsManualCode.BfsSystemId = @BfsSystemId");
                    parameters.Add("@BfsSystemId", filter.BfsSystemId.Value);
                }
if (filter.BfsComponentId.HasValue)
                {
                    sql.AppendLine("BfsManualCode.BfsComponentId = @BfsComponentId");
                    parameters.Add("@BfsComponentId", filter.BfsComponentId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsManualCodeListFilter> request, DynamicParameters parameters)
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