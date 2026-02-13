using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class DeploymentAzureList: QueryBase<DeploymentAzureListFilter>,  IDeploymentAzureList
    {
        public DeploymentAzureList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<DeploymentAzureListItem>> GetAsync(QueryRequest<DeploymentAzureListFilter> request)
        {
            var response = new QueryResponse<DeploymentAzureListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<DeploymentAzureListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<DeploymentAzureListItem>)items;

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = db.ExecuteScalar<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() { DbName = "DeploymentAzure.Project", QueryName = "Project", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.ScriptFile", QueryName = "ScriptFile", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.SourceProject", QueryName = "SourceProject", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.SourcePath", QueryName = "SourcePath", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.PublishPath", QueryName = "PublishPath", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.Config", QueryName = "Config", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.EnvironmentValue", QueryName = "EnvironmentValue", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.TargetVirtualFolder", QueryName = "TargetVirtualFolder", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.PublishProfilePath", QueryName = "PublishProfilePath", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.AppService", QueryName = "AppService", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentAzure.ResourceGroup", QueryName = "ResourceGroup", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From DeploymentAzure ");

           sql.AppendLine($"   Left Join BfsSystem on DeploymentAzure.BfsSystemId = BfsSystem.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<DeploymentAzureListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" DeploymentAzure.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (filter.BfsSystemId.HasValue)
                {
                    sql.AppendLine("DeploymentAzure.BfsSystemId = @BfsSystemId");
                    parameters.Add("@BfsSystemId", filter.BfsSystemId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<DeploymentAzureListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
       }       
    }
}