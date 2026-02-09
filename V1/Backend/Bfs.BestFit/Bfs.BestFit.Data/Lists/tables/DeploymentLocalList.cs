using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class DeploymentLocalList : IDeploymentLocalList
    {
        public DeploymentLocalList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<DeploymentLocalListItem>> GetDeploymentLocalListAsync(QueryRequest<DeploymentLocalListFilter> request)
        {
            var response = new QueryResponse<DeploymentLocalListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select DeploymentLocal.isDeleted");

                sqlSelect.AppendLine(",DeploymentLocal.isDeleted");
sqlSelect.AppendLine(",DeploymentLocal.id");
sqlSelect.AppendLine(",DeploymentLocal.scriptFile");
sqlSelect.AppendLine(",DeploymentLocal.sourceProject");
sqlSelect.AppendLine(",DeploymentLocal.sourcePath");
sqlSelect.AppendLine(",DeploymentLocal.publishPath");
sqlSelect.AppendLine(",DeploymentLocal.config");
sqlSelect.AppendLine(",DeploymentLocal.environmentValue");
sqlSelect.AppendLine(",DeploymentLocal.targetVirtualFolder");
sqlSelect.AppendLine(",DeploymentLocal.webSite");
sqlSelect.AppendLine(",DeploymentLocal.appPoolName");
sqlSelect.AppendLine(",DeploymentLocal.port");
sqlSelect.AppendLine(",DeploymentLocal.httpsRequired");
sqlSelect.AppendLine(",DeploymentLocal.project");

                sqlSelect.AppendLine(",DeploymentLocal.systemInfoId");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(DeploymentLocal.FirstName + ' ' + DeploymentLocal.LastName) as Name");

                 //lookups
                sqlSelect.AppendLine($"   ,SystemInfo.Name SystemInfo");

                sqlSelect.AppendLine($" From DeploymentLocal");
                sqlSelect.AppendLine($"   Left Join SystemInfo on DeploymentLocal.SystemInfoId = SystemInfo.Id");

                sqlSelect.AppendLine($" Where 1=1 and DeploymentLocal.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplyDeploymentLocalListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<DeploymentLocalListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<DeploymentLocalListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From DeploymentLocal");
                sqlCount.AppendLine($" Where 1=1 and DeploymentLocal.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplyDeploymentLocalListFilter(request, parameters);

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
"ScriptFile",
"SourceProject",
"SourcePath",
"PublishPath",
"Config",
"EnvironmentValue",
"TargetVirtualFolder",
"WebSite",
"AppPoolName",
"Port",
"HttpsRequired",
"Project",

                "SystemInfoId",

            };
        }
    }

    public static class DeploymentLocalListExtensions
    {
        public static DynamicParameters ApplyDeploymentLocalListFilter(this StringBuilder sql, QueryRequest<DeploymentLocalListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (filter.SystemInfoId.HasValue)
            {
                sql.Append(" AND DeploymentLocal.SystemInfoId = @SystemInfoId");
                parameters.Add("@SystemInfoId", filter.SystemInfoId.Value);
            }

            return parameters;
        }
    }

}