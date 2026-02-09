using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class ComponentList : IComponentList
    {
        public ComponentList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<ComponentListItem>> GetComponentListAsync(QueryRequest<ComponentListFilter> request)
        {
            var response = new QueryResponse<ComponentListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select Component.isDeleted");

                sqlSelect.AppendLine(",Component.isDeleted");
sqlSelect.AppendLine(",Component.id");
sqlSelect.AppendLine(",Component.isSoftDelete");
sqlSelect.AppendLine(",Component.name");
sqlSelect.AppendLine(",Component.displayName");
sqlSelect.AppendLine(",Component.menuName");
sqlSelect.AppendLine(",Component.menuPlaceHolder");
sqlSelect.AppendLine(",Component.notes");
sqlSelect.AppendLine(",Component.queryBaseTable");

                sqlSelect.AppendLine(",Component.systemInfoId");
sqlSelect.AppendLine(",Component.dataTypeId");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(Component.FirstName + ' ' + Component.LastName) as Name");

                 //lookups
                sqlSelect.AppendLine($"   ,SystemInfo.Name SystemInfo");
sqlSelect.AppendLine($"   ,DataType.Name DataType");

                sqlSelect.AppendLine($" From Component");
                sqlSelect.AppendLine($"   Left Join SystemInfo on Component.SystemInfoId = SystemInfo.Id");
sqlSelect.AppendLine($"   Left Join DataType on Component.DataTypeId = DataType.Id");

                sqlSelect.AppendLine($" Where 1=1 and Component.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplyComponentListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<ComponentListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<ComponentListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From Component");
                sqlCount.AppendLine($" Where 1=1 and Component.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplyComponentListFilter(request, parameters);

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
"IsSoftDelete",
"Name",
"DisplayName",
"MenuName",
"MenuPlaceHolder",
"Notes",
"QueryBaseTable",

                "SystemInfoId",
"DataTypeId",

            };
        }
    }

    public static class ComponentListExtensions
    {
        public static DynamicParameters ApplyComponentListFilter(this StringBuilder sql, QueryRequest<ComponentListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                sql.Append(" AND Component.Name like '%'+@Name+'%' ");
                parameters.Add("@Name", filter.Name);
            }

            if (filter.SystemInfoId.HasValue)
            {
                sql.Append(" AND Component.SystemInfoId = @SystemInfoId");
                parameters.Add("@SystemInfoId", filter.SystemInfoId.Value);
            }
if (filter.DataTypeId.HasValue)
            {
                sql.Append(" AND Component.DataTypeId = @DataTypeId");
                parameters.Add("@DataTypeId", filter.DataTypeId.Value);
            }

            return parameters;
        }
    }

}