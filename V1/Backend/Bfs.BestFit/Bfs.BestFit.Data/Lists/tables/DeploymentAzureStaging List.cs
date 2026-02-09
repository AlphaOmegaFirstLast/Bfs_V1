using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data.Interfaces;
using Bfs.BestFit.Data;
using System.Text;

namespace Bfs.BestFit.Data.Lists
{
    public class DeploymentAzureStagingList : IDeploymentAzureStagingList
    {
        public DeploymentAzureStagingList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<DeploymentAzureStagingListItem>> GetDeploymentAzureStagingListAsync(QueryRequest<DeploymentAzureStagingListFilter> request)
        {
            var response = new QueryResponse<DeploymentAzureStagingListItem>();
            using var db = new SqlConnection(_connectionString);
            {
                var sqlSelect = new StringBuilder();
                // Table Fields
                sqlSelect.AppendLine($" Select DeploymentAzureStaging.isDeleted");

                sqlSelect.AppendLine(",DeploymentAzureStaging.project");
sqlSelect.AppendLine(",DeploymentAzureStaging.isDeleted");
sqlSelect.AppendLine(",DeploymentAzureStaging.id");
sqlSelect.AppendLine(",DeploymentAzureStaging.scriptFile");
sqlSelect.AppendLine(",DeploymentAzureStaging.sourceProject");
sqlSelect.AppendLine(",DeploymentAzureStaging.sourcePath");
sqlSelect.AppendLine(",DeploymentAzureStaging.publishPath");
sqlSelect.AppendLine(",DeploymentAzureStaging.config");
sqlSelect.AppendLine(",DeploymentAzureStaging.environmentValue");
sqlSelect.AppendLine(",DeploymentAzureStaging.targetVirtualFolder");
sqlSelect.AppendLine(",DeploymentAzureStaging.publishProfilePath");
sqlSelect.AppendLine(",DeploymentAzureStaging.appService");
sqlSelect.AppendLine(",DeploymentAzureStaging.resourceGroup");

                sqlSelect.AppendLine(",DeploymentAzureStaging.systemInfoId");

                //manual: Add (or uncomment below line) list output field "Name" if there is none has been generated. for lookups & filter dropdowns
                // sqlSelect.AppendLine(",(DeploymentAzureStaging.FirstName + ' ' + DeploymentAzureStaging.LastName) as Name");

                 //lookups
                sqlSelect.AppendLine($"   ,SystemInfo.Name SystemInfo");

                sqlSelect.AppendLine($" From DeploymentAzureStaging");
                sqlSelect.AppendLine($"   Left Join SystemInfo on DeploymentAzureStaging.SystemInfoId = SystemInfo.Id");

                sqlSelect.AppendLine($" Where 1=1 and DeploymentAzureStaging.IsDeleted=0 ");

                var parameters = new DynamicParameters();
                parameters = sqlSelect.ApplyDeploymentAzureStagingListFilter(request, parameters);
                sqlSelect.ApplySort(request, GetAllowedSortFields());
                parameters = sqlSelect.ApplyPagination(request, parameters);

                // Run List
                var sqlStatement = sqlSelect.ToString();
                var items = await db.QueryAsync<DeploymentAzureStagingListItem>(sqlSelect.ToString(), parameters);
                response.Items = (List<DeploymentAzureStagingListItem>)items;

                var sqlCount = new StringBuilder();
                sqlCount.AppendLine($" Select count(1)");
                sqlCount.AppendLine($" From DeploymentAzureStaging");
                sqlCount.AppendLine($" Where 1=1 and DeploymentAzureStaging.IsDeleted=0 ");

                parameters = new DynamicParameters();
                parameters = sqlCount.ApplyDeploymentAzureStagingListFilter(request, parameters);

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
                "Project",
"IsDeleted",
"Id",
"ScriptFile",
"SourceProject",
"SourcePath",
"PublishPath",
"Config",
"EnvironmentValue",
"TargetVirtualFolder",
"PublishProfilePath",
"AppService",
"ResourceGroup",

                "SystemInfoId",

            };
        }
    }

    public static class DeploymentAzureStagingListExtensions
    {
        public static DynamicParameters ApplyDeploymentAzureStagingListFilter(this StringBuilder sql, QueryRequest<DeploymentAzureStagingListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return parameters;
            }

            if (filter.SystemInfoId.HasValue)
            {
                sql.Append(" AND DeploymentAzureStaging.SystemInfoId = @SystemInfoId");
                parameters.Add("@SystemInfoId", filter.SystemInfoId.Value);
            }

            return parameters;
        }
    }

}