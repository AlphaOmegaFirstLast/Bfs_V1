using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class ComponentBusinessActionList : IComponentBusinessActionList
    {
        public ComponentBusinessActionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<ComponentBusinessActionListItem>> GetComponentBusinessActionListAsync(QueryRequest<ComponentBusinessActionListFilter> request)
        {
            var response = new QueryResponse<ComponentBusinessActionListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select ComponentBusinessAction.isDeleted");

                sqlSelect.AppendLine(",ComponentBusinessAction.isDeleted");
sqlSelect.AppendLine(",ComponentBusinessAction.id");

                sqlSelect.AppendLine(",ComponentBusinessAction.componentId");
sqlSelect.AppendLine(",ComponentBusinessAction.businessActionId");
sqlSelect.AppendLine(",ComponentBusinessAction.actionLocationId");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(ComponentBusinessAction.FirstName + ' ' + ComponentBusinessAction.LastName) as Name");

                 //lookups
                sqlSelect.AppendLine($"   ,Component.Name Component");
sqlSelect.AppendLine($"   ,BusinessAction.Name BusinessAction");
sqlSelect.AppendLine($"   ,ActionLocation.Name ActionLocation");

                sqlSelect.AppendLine($" From ComponentBusinessAction");
                sqlSelect.AppendLine($"   Left Join Component on ComponentBusinessAction.ComponentId = Component.Id");
sqlSelect.AppendLine($"   Left Join BusinessAction on ComponentBusinessAction.BusinessActionId = BusinessAction.Id");
sqlSelect.AppendLine($"   Left Join ActionLocation on ComponentBusinessAction.ActionLocationId = ActionLocation.Id");

                sqlSelect.AppendLine($" Where 1=1 and ComponentBusinessAction.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplyComponentBusinessActionListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<ComponentBusinessActionListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<ComponentBusinessActionListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From ComponentBusinessAction");
                sqlCount.AppendLine($" Where 1=1 and ComponentBusinessAction.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplyComponentBusinessActionListFilter(request, parameters);

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

                "ComponentId",
"BusinessActionId",
"ActionLocationId",

            };
        }
    }

    public static class ComponentBusinessActionListExtensions
    {
        public static DynamicParameters ApplyComponentBusinessActionListFilter(this StringBuilder sql, QueryRequest<ComponentBusinessActionListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (filter.ComponentId.HasValue)
            {
                sql.Append(" AND ComponentBusinessAction.ComponentId = @ComponentId");
                parameters.Add("@ComponentId", filter.ComponentId.Value);
            }
if (filter.BusinessActionId.HasValue)
            {
                sql.Append(" AND ComponentBusinessAction.BusinessActionId = @BusinessActionId");
                parameters.Add("@BusinessActionId", filter.BusinessActionId.Value);
            }
if (filter.ActionLocationId.HasValue)
            {
                sql.Append(" AND ComponentBusinessAction.ActionLocationId = @ActionLocationId");
                parameters.Add("@ActionLocationId", filter.ActionLocationId.Value);
            }

            return parameters;
        }
    }

}