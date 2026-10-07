using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data.Interfaces;
using Bfs.Auth.Data;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Data.Lists
{
    public class ResourceRuleList: QueryBase<ResourceRuleListFilter>,  IResourceRuleList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public ResourceRuleList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<ResourceRuleListItem>> GetAsync(QueryRequest<ResourceRuleListFilter> request)
        {
            var response = new QueryResponse<ResourceRuleListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<ResourceRuleListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<ResourceRuleListItem> DoMapping(IEnumerable<ResourceRuleListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (ResourceRuleListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "SelectBlackList", DbName = "athResourceRule.SelectBlackList", QueryName = "SelectBlackList", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "Id", DbName = "athResourceRule.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "RoleId", DbName = "athResourceRule.RoleId", QueryName = "RoleId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "BfsComponentName", DbName = "athResourceRule.BfsComponentName", QueryName = "BfsComponentName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "JoinStatement", DbName = "athResourceRule.JoinStatement", QueryName = "JoinStatement", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "WhereStatement", DbName = "athResourceRule.WhereStatement", QueryName = "WhereStatement", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "ParameterName", DbName = "athResourceRule.ParameterName", QueryName = "ParameterName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "ParameterValue", DbName = "athResourceRule.ParameterValue", QueryName = "ParameterValue", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "ParameterType", DbName = "athResourceRule.ParameterType", QueryName = "ParameterType", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "BfsComponentId", DbName = "athResourceRule.BfsComponentId", QueryName = "BfsComponentId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "RoleName", DbName = "athResourceRule.RoleName", QueryName = "RoleName", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "Role", FieldName = "Name", DbName = "athRole.Name", QueryName = "RoleName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "Name", DbName = "BestFit_V8.dbo.BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false});

            //autoCompletes

           //Aggregates

//Template_Start_Code_DontOverwrite_5
             _fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "Name", DbName = "athResourceRule.Name", QueryName = "AutoCompleteName", IsAggregare = false});
             _fieldList.Add(new QueryField() {ComponentName = "ResourceRule", FieldName = "Name", DbName = "athResourceRule.Name", QueryName = "LookupName", IsAggregare = false});
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athResourceRule ");

           sql.AppendLine($"   Left Join athRole on athResourceRule.RoleId = athRole.Id");
sql.AppendLine($"   Left Join BestFit_V8.dbo.BfsComponent on athResourceRule.BfsComponentId = BestFit_V8.dbo.BfsComponent.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

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
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athResourceRule.SelectBlackList like '%'+@SelectBlackList+'%' ");
                      parameters.Add("@SelectBlackList", filter.SelectBlackList);                    
                   }
                   else
                   {
                      sql.AppendLine("athResourceRule.SelectBlackList like '%'+@SelectBlackList+'%' ");
                      parameters.Add("@SelectBlackList", filter.SelectBlackList);
                   }
                }
if (!string.IsNullOrEmpty(filter.BfsComponentName))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athResourceRule.BfsComponentName like '%'+@BfsComponentName+'%' ");
                      parameters.Add("@BfsComponentName", filter.BfsComponentName);                    
                   }
                   else
                   {
                      sql.AppendLine("athResourceRule.BfsComponentName like '%'+@BfsComponentName+'%' ");
                      parameters.Add("@BfsComponentName", filter.BfsComponentName);
                   }
                }
if (!string.IsNullOrEmpty(filter.JoinStatement))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athResourceRule.JoinStatement like '%'+@JoinStatement+'%' ");
                      parameters.Add("@JoinStatement", filter.JoinStatement);                    
                   }
                   else
                   {
                      sql.AppendLine("athResourceRule.JoinStatement like '%'+@JoinStatement+'%' ");
                      parameters.Add("@JoinStatement", filter.JoinStatement);
                   }
                }
if (!string.IsNullOrEmpty(filter.WhereStatement))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athResourceRule.WhereStatement like '%'+@WhereStatement+'%' ");
                      parameters.Add("@WhereStatement", filter.WhereStatement);                    
                   }
                   else
                   {
                      sql.AppendLine("athResourceRule.WhereStatement like '%'+@WhereStatement+'%' ");
                      parameters.Add("@WhereStatement", filter.WhereStatement);
                   }
                }
if (!string.IsNullOrEmpty(filter.ParameterName))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athResourceRule.ParameterName like '%'+@ParameterName+'%' ");
                      parameters.Add("@ParameterName", filter.ParameterName);                    
                   }
                   else
                   {
                      sql.AppendLine("athResourceRule.ParameterName like '%'+@ParameterName+'%' ");
                      parameters.Add("@ParameterName", filter.ParameterName);
                   }
                }
if (!string.IsNullOrEmpty(filter.ParameterValue))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athResourceRule.ParameterValue like '%'+@ParameterValue+'%' ");
                      parameters.Add("@ParameterValue", filter.ParameterValue);                    
                   }
                   else
                   {
                      sql.AppendLine("athResourceRule.ParameterValue like '%'+@ParameterValue+'%' ");
                      parameters.Add("@ParameterValue", filter.ParameterValue);
                   }
                }
if (!string.IsNullOrEmpty(filter.ParameterType))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athResourceRule.ParameterType like '%'+@ParameterType+'%' ");
                      parameters.Add("@ParameterType", filter.ParameterType);                    
                   }
                   else
                   {
                      sql.AppendLine("athResourceRule.ParameterType like '%'+@ParameterType+'%' ");
                      parameters.Add("@ParameterType", filter.ParameterType);
                   }
                }
if (!string.IsNullOrEmpty(filter.RoleName))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("athResourceRule.RoleName like '%'+@RoleName+'%' ");
                      parameters.Add("@RoleName", filter.RoleName);                    
                   }
                   else
                   {
                      sql.AppendLine("athResourceRule.RoleName like '%'+@RoleName+'%' ");
                      parameters.Add("@RoleName", filter.RoleName);
                   }
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

                //Template_Start_Code_DontOverwrite_7
                //Template_End_Code_DontOverwrite_7      
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
//Template_Start_Code_DontOverwrite_8
//Template_End_Code_DontOverwrite_8       
    }
}