using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class BusinessActionList : IBusinessActionList
    {
        public BusinessActionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BusinessActionListItem>> GetBusinessActionListAsync(QueryRequest<BusinessActionListFilter> request)
        {
            var response = new QueryResponse<BusinessActionListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select BusinessAction.isDeleted");

                sqlSelect.AppendLine(",BusinessAction.isDeleted");
sqlSelect.AppendLine(",BusinessAction.id");
sqlSelect.AppendLine(",BusinessAction.name");
sqlSelect.AppendLine(",BusinessAction.notes");

                sqlSelect.AppendLine(",BusinessAction.actionTypeId");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(BusinessAction.FirstName + ' ' + BusinessAction.LastName) as Name");

                 //lookups
                sqlSelect.AppendLine($"   ,ActionType.Name ActionType");

                sqlSelect.AppendLine($" From BusinessAction");
                sqlSelect.AppendLine($"   Left Join ActionType on BusinessAction.ActionTypeId = ActionType.Id");

                sqlSelect.AppendLine($" Where 1=1 and BusinessAction.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplyBusinessActionListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<BusinessActionListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<BusinessActionListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From BusinessAction");
                sqlCount.AppendLine($" Where 1=1 and BusinessAction.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplyBusinessActionListFilter(request, parameters);

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

                "ActionTypeId",

            };
        }
    }

    public static class BusinessActionListExtensions
    {
        public static DynamicParameters ApplyBusinessActionListFilter(this StringBuilder sql, QueryRequest<BusinessActionListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                sql.Append(" AND BusinessAction.Name like '%'+@Name+'%' ");
                parameters.Add("@Name", filter.Name);
            }

            if (filter.ActionTypeId.HasValue)
            {
                sql.Append(" AND BusinessAction.ActionTypeId = @ActionTypeId");
                parameters.Add("@ActionTypeId", filter.ActionTypeId.Value);
            }

            return parameters;
        }
    }

}