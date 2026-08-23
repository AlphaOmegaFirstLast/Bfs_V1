using Bfs.Core.Data;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;

namespace Bfs.Auth.Data.Lists
{
    public class ResourceRuleList: QueryBase<ResourceRuleListFilter>,  IResourceRuleList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public ResourceRuleList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<ResourceRuleListItem>> GetAsync(QueryRequest<ResourceRuleListFilter> request)
        {
            var response = new QueryResponse<ResourceRuleListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<ResourceRuleListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<ResourceRuleListItem>)items;

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "SelectBlackList", DbName = "athResourceRule.SelectBlackList", QueryName = "ResourceRule_SelectBlackList", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "Id", DbName = "athResourceRule.Id", QueryName = "ResourceRule_Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "RoleId", DbName = "athResourceRule.RoleId", QueryName = "ResourceRule_RoleId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "BfsComponentName", DbName = "athResourceRule.BfsComponentName", QueryName = "ResourceRule_BfsComponentName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "JoinStatement", DbName = "athResourceRule.JoinStatement", QueryName = "ResourceRule_JoinStatement", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "WhereStatement", DbName = "athResourceRule.WhereStatement", QueryName = "ResourceRule_WhereStatement", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "ParameterName", DbName = "athResourceRule.ParameterName", QueryName = "ResourceRule_ParameterName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "ParameterValue", DbName = "athResourceRule.ParameterValue", QueryName = "ResourceRule_ParameterValue", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "ParameterType", DbName = "athResourceRule.ParameterType", QueryName = "ResourceRule_ParameterType", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "BfsComponentId", DbName = "athResourceRule.BfsComponentId", QueryName = "ResourceRule_BfsComponentId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "RoleName", DbName = "athResourceRule.RoleName", QueryName = "ResourceRule_RoleName", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "Role", FieldName = "Name", DbName = "athRole.Name", QueryName = "RoleName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "Name", DbName = "BestFit_V6.dbo.BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athResourceRule ");

           sql.AppendLine($"   Left Join athRole on athResourceRule.RoleId = athRole.Id");
sql.AppendLine($"   Left Join BestFit_V6.dbo.BfsComponent on athResourceRule.BfsComponentId = BestFit_V6.dbo.BfsComponent.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<ResourceRuleListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" athResourceRule.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("athResourceRule.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.SelectBlackList))
                {
                    sql.AppendLine("athResourceRule.SelectBlackList like '%'+@SelectBlackList+'%' ");
                    parameters.Add("@SelectBlackList", filter.SelectBlackList);
                }
if (!string.IsNullOrEmpty(filter.BfsComponentName))
                {
                    sql.AppendLine("athResourceRule.BfsComponentName like '%'+@BfsComponentName+'%' ");
                    parameters.Add("@BfsComponentName", filter.BfsComponentName);
                }
if (!string.IsNullOrEmpty(filter.JoinStatement))
                {
                    sql.AppendLine("athResourceRule.JoinStatement like '%'+@JoinStatement+'%' ");
                    parameters.Add("@JoinStatement", filter.JoinStatement);
                }
if (!string.IsNullOrEmpty(filter.WhereStatement))
                {
                    sql.AppendLine("athResourceRule.WhereStatement like '%'+@WhereStatement+'%' ");
                    parameters.Add("@WhereStatement", filter.WhereStatement);
                }
if (!string.IsNullOrEmpty(filter.ParameterName))
                {
                    sql.AppendLine("athResourceRule.ParameterName like '%'+@ParameterName+'%' ");
                    parameters.Add("@ParameterName", filter.ParameterName);
                }
if (!string.IsNullOrEmpty(filter.ParameterValue))
                {
                    sql.AppendLine("athResourceRule.ParameterValue like '%'+@ParameterValue+'%' ");
                    parameters.Add("@ParameterValue", filter.ParameterValue);
                }
if (!string.IsNullOrEmpty(filter.ParameterType))
                {
                    sql.AppendLine("athResourceRule.ParameterType like '%'+@ParameterType+'%' ");
                    parameters.Add("@ParameterType", filter.ParameterType);
                }
if (!string.IsNullOrEmpty(filter.RoleName))
                {
                    sql.AppendLine("athResourceRule.RoleName like '%'+@RoleName+'%' ");
                    parameters.Add("@RoleName", filter.RoleName);
                }

                if (filter.RoleId.HasValue)
                {
                    sql.AppendLine("athResourceRule.RoleId = @RoleId");
                    parameters.Add("@RoleId", filter.RoleId.Value);
                }
if (filter.BfsComponentId.HasValue)
                {
                    sql.AppendLine("athResourceRule.BfsComponentId = @BfsComponentId");
                    parameters.Add("@BfsComponentId", filter.BfsComponentId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<ResourceRuleListFilter> request, DynamicParameters parameters)
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

