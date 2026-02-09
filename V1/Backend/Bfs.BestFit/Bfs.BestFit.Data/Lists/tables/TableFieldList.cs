using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class TableFieldList : ITableFieldList
    {
        public TableFieldList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<TableFieldListItem>> GetTableFieldListAsync(QueryRequest<TableFieldListFilter> request)
        {
            var response = new QueryResponse<TableFieldListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select TableField.isDeleted");

                sqlSelect.AppendLine(",TableField.isDeleted");
sqlSelect.AppendLine(",TableField.id");
sqlSelect.AppendLine(",TableField.field");
sqlSelect.AppendLine(",TableField.displayName");
sqlSelect.AppendLine(",TableField.isQueryColumn");
sqlSelect.AppendLine(",TableField.isJoinField");
sqlSelect.AppendLine(",TableField.parentTable");
sqlSelect.AppendLine(",TableField.uiFormControlOrder");

                sqlSelect.AppendLine(",TableField.componentId");
sqlSelect.AppendLine(",TableField.filterTypeId");
sqlSelect.AppendLine(",TableField.backendDataTypeId");
sqlSelect.AppendLine(",TableField.formControlTypeId");

                sqlSelect.AppendLine(",TableField.fieldValidation");
sqlSelect.AppendLine(",TableField.reportInfo");
sqlSelect.AppendLine(",TableField.matrixInfo");
sqlSelect.AppendLine(",TableField.toolTipInfo");
sqlSelect.AppendLine(",TableField.formInfo");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(TableField.FirstName + ' ' + TableField.LastName) as Name");

                 //lookups
                sqlSelect.AppendLine($"   ,Component.Name Component");
sqlSelect.AppendLine($"   ,FilterType.Name FilterType");
sqlSelect.AppendLine($"   ,BackendDataType.Name BackendDataType");
sqlSelect.AppendLine($"   ,FormControlType.Name FormControlType");

                sqlSelect.AppendLine($" From TableField");
                sqlSelect.AppendLine($"   Left Join Component on TableField.ComponentId = Component.Id");
sqlSelect.AppendLine($"   Left Join FilterType on TableField.FilterTypeId = FilterType.Id");
sqlSelect.AppendLine($"   Left Join BackendDataType on TableField.BackendDataTypeId = BackendDataType.Id");
sqlSelect.AppendLine($"   Left Join FormControlType on TableField.FormControlTypeId = FormControlType.Id");

                sqlSelect.AppendLine($" Where 1=1 and TableField.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplyTableFieldListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<TableFieldListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<TableFieldListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From TableField");
                sqlCount.AppendLine($" Where 1=1 and TableField.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplyTableFieldListFilter(request, parameters);

                // Run Count
                var sqlCountStatement = sqlCount.ToString();
                response.TotalItems = db.ExecuteScalar<long>(sqlCountStatement, parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<string> GetAllowedSortFields()
        {
            return new List<string>() {
                "IsDeleted",
"Id",
"Field",
"DisplayName",
"IsQueryColumn",
"IsJoinField",
"ParentTable",
"UiFormControlOrder",

                "ComponentId",
"FilterTypeId",
"BackendDataTypeId",
"FormControlTypeId",

            };
        }
    }

    public static class TableFieldListExtensions
    {
        public static DynamicParameters ApplyTableFieldListFilter(this StringBuilder sql, QueryRequest<TableFieldListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (!string.IsNullOrEmpty(filter.Field))
            {
                sql.Append(" AND TableField.Field like '%'+@Field+'%' ");
                parameters.Add("@Field", filter.Field);
            }

            if (filter.ComponentId.HasValue)
            {
                sql.Append(" AND TableField.ComponentId = @ComponentId");
                parameters.Add("@ComponentId", filter.ComponentId.Value);
            }
if (filter.FilterTypeId.HasValue)
            {
                sql.Append(" AND TableField.FilterTypeId = @FilterTypeId");
                parameters.Add("@FilterTypeId", filter.FilterTypeId.Value);
            }
if (filter.BackendDataTypeId.HasValue)
            {
                sql.Append(" AND TableField.BackendDataTypeId = @BackendDataTypeId");
                parameters.Add("@BackendDataTypeId", filter.BackendDataTypeId.Value);
            }
if (filter.FormControlTypeId.HasValue)
            {
                sql.Append(" AND TableField.FormControlTypeId = @FormControlTypeId");
                parameters.Add("@FormControlTypeId", filter.FormControlTypeId.Value);
            }

            return parameters;
        }
    }

}