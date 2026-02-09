using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class ClientList : IClientList
    {
        public ClientList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<ClientListItem>> GetClientListAsync(QueryRequest<ClientListFilter> request)
        {
            var response = new QueryResponse<ClientListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select Client.isDeleted");

                sqlSelect.AppendLine(",Client.isDeleted");
sqlSelect.AppendLine(",Client.id");
sqlSelect.AppendLine(",Client.name");
sqlSelect.AppendLine(",Client.notes");
sqlSelect.AppendLine(",Client.dbConnection");

                sqlSelect.AppendLine(",Client.customFields");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(Client.FirstName + ' ' + Client.LastName) as Name");

                 //lookups

                sqlSelect.AppendLine($" From Client");

                sqlSelect.AppendLine($" Where 1=1 and Client.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplyClientListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<ClientListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<ClientListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From Client");
                sqlCount.AppendLine($" Where 1=1 and Client.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplyClientListFilter(request, parameters);

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
"DbConnection",

            };
        }
    }

    public static class ClientListExtensions
    {
        public static DynamicParameters ApplyClientListFilter(this StringBuilder sql, QueryRequest<ClientListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                sql.Append(" AND Client.Name like '%'+@Name+'%' ");
                parameters.Add("@Name", filter.Name);
            }

            return parameters;
        }
    }

}