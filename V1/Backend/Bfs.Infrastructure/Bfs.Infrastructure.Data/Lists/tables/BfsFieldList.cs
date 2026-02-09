using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class BfsFieldList: QueryBase<BfsFieldListFilter>,  IBfsFieldList
    {
        public BfsFieldList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsFieldListItem>> GetAsync(QueryRequest<BfsFieldListFilter> request)
        {
            var response = new QueryResponse<BfsFieldListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsFieldListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BfsFieldListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "BfsField.FieldValidation", QueryName = "BfsFieldFieldValidation", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.Id", QueryName = "BfsFieldId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.BfsComponentId", QueryName = "BfsFieldBfsComponentId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.Field", QueryName = "BfsFieldField", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.DisplayName", QueryName = "BfsFieldDisplayName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.IsQueryColumn", QueryName = "BfsFieldIsQueryColumn", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.IsJoinField", QueryName = "BfsFieldIsJoinField", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.ParentTable", QueryName = "BfsFieldParentTable", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.FilterTypeId", QueryName = "BfsFieldFilterTypeId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.BackendDataTypeId", QueryName = "BfsFieldBackendDataTypeId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.ReportInfo", QueryName = "BfsFieldReportInfo", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.MatrixInfo", QueryName = "BfsFieldMatrixInfo", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.ToolTipInfo", QueryName = "BfsFieldToolTipInfo", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsField.FormInfo", QueryName = "BfsFieldFormInfo", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "FilterType.Name", QueryName = "FilterTypeName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BackendDataType.Name", QueryName = "BackendDataTypeName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsField ");

           sql.AppendLine($"   Left Join BfsComponent on BfsField.BfsComponentId = BfsComponent.Id");
sql.AppendLine($"   Left Join FilterType on BfsField.FilterTypeId = FilterType.Id");
sql.AppendLine($"   Left Join BackendDataType on BfsField.BackendDataTypeId = BackendDataType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsFieldListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsField.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.Field))
                {
                    sql.AppendLine("BfsField.Field like '%'+@Field+'%' ");
                    parameters.Add("@Field", filter.Field);
                }

                if (filter.BfsComponentId.HasValue)
                {
                    sql.AppendLine("BfsField.BfsComponentId = @BfsComponentId");
                    parameters.Add("@BfsComponentId", filter.BfsComponentId.Value);
                }
if (filter.FilterTypeId.HasValue)
                {
                    sql.AppendLine("BfsField.FilterTypeId = @FilterTypeId");
                    parameters.Add("@FilterTypeId", filter.FilterTypeId.Value);
                }
if (filter.BackendDataTypeId.HasValue)
                {
                    sql.AppendLine("BfsField.BackendDataTypeId = @BackendDataTypeId");
                    parameters.Add("@BackendDataTypeId", filter.BackendDataTypeId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsFieldListFilter> request, DynamicParameters parameters)
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