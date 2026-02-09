using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class SystemInfoList : ISystemInfoList
    {
        public SystemInfoList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<SystemInfoListItem>> GetSystemInfoListAsync(QueryRequest<SystemInfoListFilter> request)
        {
            var response = new QueryResponse<SystemInfoListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select SystemInfo.isDeleted");

                sqlSelect.AppendLine(",SystemInfo.isDeleted");
sqlSelect.AppendLine(",SystemInfo.id");
sqlSelect.AppendLine(",SystemInfo.name");
sqlSelect.AppendLine(",SystemInfo.notes");
sqlSelect.AppendLine(",SystemInfo.basePortNumber");

                sqlSelect.AppendLine(",SystemInfo.clientId");
sqlSelect.AppendLine(",SystemInfo.systemTemplateId");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(SystemInfo.FirstName + ' ' + SystemInfo.LastName) as Name");

                 //lookups
                sqlSelect.AppendLine($"   ,Client.Name Client");
sqlSelect.AppendLine($"   ,SystemTemplate.Name SystemTemplate");

                sqlSelect.AppendLine($" From SystemInfo");
                sqlSelect.AppendLine($"   Left Join Client on SystemInfo.ClientId = Client.Id");
sqlSelect.AppendLine($"   Left Join SystemTemplate on SystemInfo.SystemTemplateId = SystemTemplate.Id");

                sqlSelect.AppendLine($" Where 1=1 and SystemInfo.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplySystemInfoListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<SystemInfoListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<SystemInfoListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From SystemInfo");
                sqlCount.AppendLine($" Where 1=1 and SystemInfo.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplySystemInfoListFilter(request, parameters);

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
"BasePortNumber",

                "ClientId",
"SystemTemplateId",

            };
        }
    }

    public static class SystemInfoListExtensions
    {
        public static DynamicParameters ApplySystemInfoListFilter(this StringBuilder sql, QueryRequest<SystemInfoListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                sql.Append(" AND SystemInfo.Name like '%'+@Name+'%' ");
                parameters.Add("@Name", filter.Name);
            }

            if (filter.ClientId.HasValue)
            {
                sql.Append(" AND SystemInfo.ClientId = @ClientId");
                parameters.Add("@ClientId", filter.ClientId.Value);
            }
if (filter.SystemTemplateId.HasValue)
            {
                sql.Append(" AND SystemInfo.SystemTemplateId = @SystemTemplateId");
                parameters.Add("@SystemTemplateId", filter.SystemTemplateId.Value);
            }

            return parameters;
        }
    }

}