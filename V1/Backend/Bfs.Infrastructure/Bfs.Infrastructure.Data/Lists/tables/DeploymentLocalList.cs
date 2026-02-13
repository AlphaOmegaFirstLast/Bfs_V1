using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class DeploymentLocalList: QueryBase<DeploymentLocalListFilter>,  IDeploymentLocalList
    {
        public DeploymentLocalList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<DeploymentLocalListItem>> GetAsync(QueryRequest<DeploymentLocalListFilter> request)
        {
            var response = new QueryResponse<DeploymentLocalListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<DeploymentLocalListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<DeploymentLocalListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "DeploymentLocal.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.ScriptFile", QueryName = "ScriptFile", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.SourceProject", QueryName = "SourceProject", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.SourcePath", QueryName = "SourcePath", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.PublishPath", QueryName = "PublishPath", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.Config", QueryName = "Config", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.EnvironmentValue", QueryName = "EnvironmentValue", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.TargetVirtualFolder", QueryName = "TargetVirtualFolder", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.WebSite", QueryName = "WebSite", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.AppPoolName", QueryName = "AppPoolName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.Port", QueryName = "Port", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.HttpsRequired", QueryName = "HttpsRequired", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DeploymentLocal.Project", QueryName = "Project", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From DeploymentLocal ");

           sql.AppendLine($"   Left Join BfsSystem on DeploymentLocal.BfsSystemId = BfsSystem.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<DeploymentLocalListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" DeploymentLocal.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (filter.BfsSystemId.HasValue)
                {
                    sql.AppendLine("DeploymentLocal.BfsSystemId = @BfsSystemId");
                    parameters.Add("@BfsSystemId", filter.BfsSystemId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<DeploymentLocalListFilter> request, DynamicParameters parameters)
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