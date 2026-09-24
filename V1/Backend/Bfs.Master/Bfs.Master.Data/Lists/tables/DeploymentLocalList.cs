using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data;
using System.Text;

namespace Bfs.Master.Data.Lists
{
    public class DeploymentLocalList: QueryBase<DeploymentLocalListFilter>,  IDeploymentLocalList
    {
        private readonly IResourceSecurity? _resourceSecurity;

        public DeploymentLocalList(string connectionString, IResourceSecurity? resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<DeploymentLocalListItem>> GetAsync(QueryRequest<DeploymentLocalListFilter> request)
        {
            var response = new QueryResponse<DeploymentLocalListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<DeploymentLocalListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<DeploymentLocalListItem> DoMapping(IEnumerable<DeploymentLocalListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (DeploymentLocalListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "Id", DbName = "DeploymentLocal.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "ScriptFile", DbName = "DeploymentLocal.ScriptFile", QueryName = "ScriptFile", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "BfsSystemId", DbName = "DeploymentLocal.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "SourceProject", DbName = "DeploymentLocal.SourceProject", QueryName = "SourceProject", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "SourcePath", DbName = "DeploymentLocal.SourcePath", QueryName = "SourcePath", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "PublishPath", DbName = "DeploymentLocal.PublishPath", QueryName = "PublishPath", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "Config", DbName = "DeploymentLocal.Config", QueryName = "Config", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "EnvironmentValue", DbName = "DeploymentLocal.EnvironmentValue", QueryName = "EnvironmentValue", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "TargetVirtualDir", DbName = "DeploymentLocal.TargetVirtualDir", QueryName = "TargetVirtualDir", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "WebSite", DbName = "DeploymentLocal.WebSite", QueryName = "WebSite", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "AppPoolName", DbName = "DeploymentLocal.AppPoolName", QueryName = "AppPoolName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "Port", DbName = "DeploymentLocal.Port", QueryName = "Port", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentLocal", FieldName = "IsHttpsRequired", DbName = "DeploymentLocal.IsHttpsRequired", QueryName = "IsHttpsRequired", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "Name", DbName = "BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false});

            //autoCompletes

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
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("DeploymentLocal.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

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

