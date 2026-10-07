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
    public class RoleComponentSystemActionList: QueryBase<RoleComponentSystemActionListFilter>,  IRoleComponentSystemActionList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public RoleComponentSystemActionList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<RoleComponentSystemActionListItem>> GetAsync(QueryRequest<RoleComponentSystemActionListFilter> request)
        {
            var response = new QueryResponse<RoleComponentSystemActionListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<RoleComponentSystemActionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<RoleComponentSystemActionListItem> DoMapping(IEnumerable<RoleComponentSystemActionListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (RoleComponentSystemActionListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "RoleComponentSystemAction", FieldName = "Id", DbName = "athRoleComponentSystemAction.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "RoleComponentSystemAction", FieldName = "BfsComponentId", DbName = "athRoleComponentSystemAction.BfsComponentId", QueryName = "BfsComponentId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "RoleComponentSystemAction", FieldName = "SystemActionId", DbName = "athRoleComponentSystemAction.SystemActionId", QueryName = "SystemActionId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "RoleComponentSystemAction", FieldName = "RoleId", DbName = "athRoleComponentSystemAction.RoleId", QueryName = "RoleId", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "Name", DbName = "BestFit_V8.dbo.BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "Name", DbName = "BestFit_V8.dbo.SystemAction.Name", QueryName = "SystemActionName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Role", FieldName = "Name", DbName = "athRole.Name", QueryName = "RoleName", IsAggregare = false});

            //autoCompletes

           //Aggregates

//Template_Start_Code_DontOverwrite_5
             _fieldList.Add(new QueryField() {ComponentName = "RoleComponentSystemAction", FieldName = "Name", DbName = "athRoleComponentSystemAction.Name", QueryName = "AutoCompleteName", IsAggregare = false});
             _fieldList.Add(new QueryField() {ComponentName = "RoleComponentSystemAction", FieldName = "Name", DbName = "athRoleComponentSystemAction.Name", QueryName = "LookupName", IsAggregare = false});
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athRoleComponentSystemAction ");

           sql.AppendLine($"   Left Join BestFit_V8.dbo.BfsComponent on athRoleComponentSystemAction.BfsComponentId = BestFit_V8.dbo.BfsComponent.Id");
sql.AppendLine($"   Left Join BestFit_V8.dbo.SystemAction on athRoleComponentSystemAction.SystemActionId = BestFit_V8.dbo.SystemAction.Id");
sql.AppendLine($"   Left Join athRole on athRoleComponentSystemAction.RoleId = athRole.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<RoleComponentSystemActionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" athRoleComponentSystemAction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("athRoleComponentSystemAction.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (filter.BfsComponentId.HasValue)
                {
                    sql.AppendLine("athRoleComponentSystemAction.BfsComponentId = @BfsComponentId");
                    parameters.Add("@BfsComponentId", filter.BfsComponentId.Value);
                }
if (filter.SystemActionId.HasValue)
                {
                    sql.AppendLine("athRoleComponentSystemAction.SystemActionId = @SystemActionId");
                    parameters.Add("@SystemActionId", filter.SystemActionId.Value);
                }
if (filter.RoleId.HasValue)
                {
                    sql.AppendLine("athRoleComponentSystemAction.RoleId = @RoleId");
                    parameters.Add("@RoleId", filter.RoleId.Value);
                }

                //Template_Start_Code_DontOverwrite_7
                //Template_End_Code_DontOverwrite_7      
            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<RoleComponentSystemActionListFilter> request, DynamicParameters parameters)
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