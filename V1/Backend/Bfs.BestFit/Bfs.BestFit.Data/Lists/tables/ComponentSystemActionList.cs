using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class ComponentSystemActionList : IComponentSystemActionList
    {
        public ComponentSystemActionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<ComponentSystemActionListItem>> GetComponentSystemActionListAsync(QueryRequest<ComponentSystemActionListFilter> request)
        {
            var response = new QueryResponse<ComponentSystemActionListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select ComponentSystemAction.isDeleted");

                sqlSelect.AppendLine(",ComponentSystemAction.isDeleted");
sqlSelect.AppendLine(",ComponentSystemAction.id");

                sqlSelect.AppendLine(",ComponentSystemAction.componentId");
sqlSelect.AppendLine(",ComponentSystemAction.systemActionId");
sqlSelect.AppendLine(",ComponentSystemAction.actionLocationId");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(ComponentSystemAction.FirstName + ' ' + ComponentSystemAction.LastName) as Name");

                 //lookups
                sqlSelect.AppendLine($"   ,Component.Name Component");
sqlSelect.AppendLine($"   ,SystemAction.Name SystemAction");
sqlSelect.AppendLine($"   ,ActionLocation.Name ActionLocation");

                sqlSelect.AppendLine($" From ComponentSystemAction");
                sqlSelect.AppendLine($"   Left Join Component on ComponentSystemAction.ComponentId = Component.Id");
sqlSelect.AppendLine($"   Left Join SystemAction on ComponentSystemAction.SystemActionId = SystemAction.Id");
sqlSelect.AppendLine($"   Left Join ActionLocation on ComponentSystemAction.ActionLocationId = ActionLocation.Id");

                sqlSelect.AppendLine($" Where 1=1 and ComponentSystemAction.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplyComponentSystemActionListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<ComponentSystemActionListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<ComponentSystemActionListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From ComponentSystemAction");
                sqlCount.AppendLine($" Where 1=1 and ComponentSystemAction.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplyComponentSystemActionListFilter(request, parameters);

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
"SystemActionId",
"ActionLocationId",

            };
        }
    }

    public static class ComponentSystemActionListExtensions
    {
        public static DynamicParameters ApplyComponentSystemActionListFilter(this StringBuilder sql, QueryRequest<ComponentSystemActionListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (filter.ComponentId.HasValue)
            {
                sql.Append(" AND ComponentSystemAction.ComponentId = @ComponentId");
                parameters.Add("@ComponentId", filter.ComponentId.Value);
            }
if (filter.SystemActionId.HasValue)
            {
                sql.Append(" AND ComponentSystemAction.SystemActionId = @SystemActionId");
                parameters.Add("@SystemActionId", filter.SystemActionId.Value);
            }
if (filter.ActionLocationId.HasValue)
            {
                sql.Append(" AND ComponentSystemAction.ActionLocationId = @ActionLocationId");
                parameters.Add("@ActionLocationId", filter.ActionLocationId.Value);
            }

            return parameters;
        }
    }

}