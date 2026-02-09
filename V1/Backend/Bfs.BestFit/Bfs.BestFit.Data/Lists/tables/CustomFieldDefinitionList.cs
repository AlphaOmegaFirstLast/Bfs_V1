using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class CustomFieldDefinitionList : ICustomFieldDefinitionList
    {
        public CustomFieldDefinitionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<CustomFieldDefinitionListItem>> GetCustomFieldDefinitionListAsync(QueryRequest<CustomFieldDefinitionListFilter> request)
        {
            var response = new QueryResponse<CustomFieldDefinitionListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select CustomFieldDefinition.isDeleted");

                sqlSelect.AppendLine(",CustomFieldDefinition.isDeleted");
sqlSelect.AppendLine(",CustomFieldDefinition.id");
sqlSelect.AppendLine(",CustomFieldDefinition.name");
sqlSelect.AppendLine(",CustomFieldDefinition.notes");
sqlSelect.AppendLine(",CustomFieldDefinition.displayName");

                sqlSelect.AppendLine(",CustomFieldDefinition.componentId");

                sqlSelect.AppendLine(",CustomFieldDefinition.fieldValidation");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(CustomFieldDefinition.FirstName + ' ' + CustomFieldDefinition.LastName) as Name");

                 //lookups
                sqlSelect.AppendLine($"   ,Component.Name Component");

                sqlSelect.AppendLine($" From CustomFieldDefinition");
                sqlSelect.AppendLine($"   Left Join Component on CustomFieldDefinition.ComponentId = Component.Id");

                sqlSelect.AppendLine($" Where 1=1 and CustomFieldDefinition.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplyCustomFieldDefinitionListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<CustomFieldDefinitionListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<CustomFieldDefinitionListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From CustomFieldDefinition");
                sqlCount.AppendLine($" Where 1=1 and CustomFieldDefinition.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplyCustomFieldDefinitionListFilter(request, parameters);

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
"Name",
"Notes",
"DisplayName",

                "ComponentId",

            };
        }
    }

    public static class CustomFieldDefinitionListExtensions
    {
        public static DynamicParameters ApplyCustomFieldDefinitionListFilter(this StringBuilder sql, QueryRequest<CustomFieldDefinitionListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                sql.Append(" AND CustomFieldDefinition.Name like '%'+@Name+'%' ");
                parameters.Add("@Name", filter.Name);
            }

            if (filter.ComponentId.HasValue)
            {
                sql.Append(" AND CustomFieldDefinition.ComponentId = @ComponentId");
                parameters.Add("@ComponentId", filter.ComponentId.Value);
            }

            return parameters;
        }
    }

}