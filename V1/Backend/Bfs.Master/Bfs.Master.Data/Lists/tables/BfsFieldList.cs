using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;
using Bfs.Master.Data;
using Bfs.Master.Data.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Bfs.Master.Data.Lists
{
    public class BfsFieldList : QueryBase<BfsFieldListFilter>, IBfsFieldList
    {
        private readonly IResourceSecurity? _resourceSecurity;

        public BfsFieldList(string connectionString, IResourceSecurity? resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsFieldListItem>> GetAsync(QueryRequest<BfsFieldListFilter> request)
        {
            var response = new QueryResponse<BfsFieldListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsFieldListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BfsFieldListItem>)items;
                response.Items = response.Items.Select(item =>
                {
                    item.FieldValidation = SerializationHelper.GetParsed<BfsFieldListItem, FieldValidation>(item, "jsonFieldValidation");
                    item.ReportInfo = SerializationHelper.GetParsed<BfsFieldListItem, ReportInfo>(item, "jsonReportInfo");
                    item.MatrixInfo = SerializationHelper.GetParsed<BfsFieldListItem, MatrixInfo>(item, "jsonMatrixInfo");
                    item.ToolTipInfo = SerializationHelper.GetParsed<BfsFieldListItem, ToolTipInfo>(item, "jsonToolTipInfo");
                    item.FormInfo = SerializationHelper.GetParsed<BfsFieldListItem, FormInfo>(item, "jsonFormInfo");
                    return item;
                }).ToList();

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
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "Id", DbName = "BfsField.Id", QueryName = "Id", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "BfsComponentId", DbName = "BfsField.BfsComponentId", QueryName = "BfsComponentId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "Field", DbName = "BfsField.Field", QueryName = "Field", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "DisplayName", DbName = "BfsField.DisplayName", QueryName = "DisplayName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "FilterTypeId", DbName = "BfsField.FilterTypeId", QueryName = "FilterTypeId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "BackendDataTypeId", DbName = "BfsField.BackendDataTypeId", QueryName = "BackendDataTypeId", IsAggregare = false });

            //objectFields. Dapper reads json fields as strings
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "FieldValidation", DbName = "BfsField.FieldValidation", QueryName = "jsonFieldValidation", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "ReportInfo", DbName = "BfsField.ReportInfo", QueryName = "jsonReportInfo", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "MatrixInfo", DbName = "BfsField.MatrixInfo", QueryName = "jsonMatrixInfo", IsAggregare = false });     
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "ToolTipInfo", DbName = "BfsField.ToolTipInfo", QueryName = "jsonToolTipInfo", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "BfsField", FieldName = "FormInfo", DbName = "BfsField.FormInfo", QueryName = "jsonFormInfo", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { ComponentName = "BfsComponent", FieldName = "Name", DbName = "BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "FilterType", FieldName = "Name", DbName = "FilterType.Name", QueryName = "FilterTypeName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "BackendDataType", FieldName = "Name", DbName = "BackendDataType.Name", QueryName = "BackendDataTypeName", IsAggregare = false });

            //autoCompletes

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
            var sql = new StringBuilder();
            sql.AppendLine(" BfsField.isDeleted=0 ");

            var filter = request.Filter;
            if (filter != null)
            {
                if ((filter.Id.HasValue) && (filter.Id > 0))
                {
                    sql.AppendLine("BfsField.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

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

