using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Lists
{
    public class DeploymentAzureList: QueryBase<DeploymentAzureListFilter>,  IDeploymentAzureList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public DeploymentAzureList(string connectionString, IResourceSecurity? resourceSecurity
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

        )
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

        }

        private readonly string _connectionString;

        public async Task<QueryResponse<DeploymentAzureListItem>> GetAsync(QueryRequest<DeploymentAzureListFilter> request)
        {
            var response = new QueryResponse<DeploymentAzureListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<DeploymentAzureListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<DeploymentAzureListItem> DoMapping(IEnumerable<DeploymentAzureListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (DeploymentAzureListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "Id", DbName = "DeploymentAzure.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "ScriptFile", DbName = "DeploymentAzure.ScriptFile", QueryName = "ScriptFile", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "BfsSystemId", DbName = "DeploymentAzure.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "SourceProject", DbName = "DeploymentAzure.SourceProject", QueryName = "SourceProject", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "SourcePath", DbName = "DeploymentAzure.SourcePath", QueryName = "SourcePath", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "PublishPath", DbName = "DeploymentAzure.PublishPath", QueryName = "PublishPath", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "Config", DbName = "DeploymentAzure.Config", QueryName = "Config", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "EnvironmentValue", DbName = "DeploymentAzure.EnvironmentValue", QueryName = "EnvironmentValue", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "TargetVirtualDir", DbName = "DeploymentAzure.TargetVirtualDir", QueryName = "TargetVirtualDir", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "PublishProfilePath", DbName = "DeploymentAzure.PublishProfilePath", QueryName = "PublishProfilePath", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "AppService", DbName = "DeploymentAzure.AppService", QueryName = "AppService", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DeploymentAzure", FieldName = "ResourceGroup", DbName = "DeploymentAzure.ResourceGroup", QueryName = "ResourceGroup", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "Name", DbName = "BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false});

            //autoCompletes

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
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("DeploymentAzure.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

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
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

    }
}