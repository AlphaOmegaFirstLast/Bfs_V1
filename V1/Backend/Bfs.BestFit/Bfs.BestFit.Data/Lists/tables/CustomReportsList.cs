using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class CustomReportsList : ICustomReportsList
    {
        public CustomReportsList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<CustomReportsListItem>> GetCustomReportsListAsync(QueryRequest<CustomReportsListFilter> request)
        {
            var response = new QueryResponse<CustomReportsListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select CustomReports.isDeleted");

                sqlSelect.AppendLine(",CustomReports.id");
sqlSelect.AppendLine(",CustomReports.name");
sqlSelect.AppendLine(",CustomReports.request");
sqlSelect.AppendLine(",CustomReports.baseReport");
sqlSelect.AppendLine(",CustomReports.isPrivate");
sqlSelect.AppendLine(",CustomReports.isDeleted");
sqlSelect.AppendLine(",CustomReports.createdBy");
sqlSelect.AppendLine(",CustomReports.url");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(CustomReports.FirstName + ' ' + CustomReports.LastName) as Name");

                 //lookups

                sqlSelect.AppendLine($" From CustomReports");

                sqlSelect.AppendLine($" Where 1=1 and CustomReports.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplyCustomReportsListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<CustomReportsListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<CustomReportsListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From CustomReports");
                sqlCount.AppendLine($" Where 1=1 and CustomReports.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplyCustomReportsListFilter(request, parameters);

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
                "Id",
"Name",
"Request",
"BaseReport",
"IsPrivate",
"IsDeleted",
"CreatedBy",
"Url",

            };
        }
    }

    public static class CustomReportsListExtensions
    {
        public static DynamicParameters ApplyCustomReportsListFilter(this StringBuilder sql, QueryRequest<CustomReportsListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                sql.Append(" AND CustomReports.Name like '%'+@Name+'%' ");
                parameters.Add("@Name", filter.Name);
            }

            return parameters;
        }
    }

}