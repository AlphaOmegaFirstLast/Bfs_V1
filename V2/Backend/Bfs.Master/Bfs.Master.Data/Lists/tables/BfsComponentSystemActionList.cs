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
    public class BfsComponentSystemActionList: QueryBase<BfsComponentSystemActionListFilter>,  IBfsComponentSystemActionList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public BfsComponentSystemActionList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<BfsComponentSystemActionListItem>> GetAsync(QueryRequest<BfsComponentSystemActionListFilter> request)
        {
            var response = new QueryResponse<BfsComponentSystemActionListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsComponentSystemActionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<BfsComponentSystemActionListItem> DoMapping(IEnumerable<BfsComponentSystemActionListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (BfsComponentSystemActionListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "BfsComponentSystemAction", FieldName = "Id", DbName = "BfsComponentSystemAction.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponentSystemAction", FieldName = "BfsComponentId", DbName = "BfsComponentSystemAction.BfsComponentId", QueryName = "BfsComponentId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponentSystemAction", FieldName = "SystemActionId", DbName = "BfsComponentSystemAction.SystemActionId", QueryName = "SystemActionId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponentSystemAction", FieldName = "ActionLocationId", DbName = "BfsComponentSystemAction.ActionLocationId", QueryName = "ActionLocationId", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "Name", DbName = "BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "Name", DbName = "SystemAction.Name", QueryName = "SystemActionName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ActionLocation", FieldName = "Name", DbName = "ActionLocation.Name", QueryName = "ActionLocationName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsComponentSystemAction ");

           sql.AppendLine($"   Left Join BfsComponent on BfsComponentSystemAction.BfsComponentId = BfsComponent.Id");
sql.AppendLine($"   Left Join SystemAction on BfsComponentSystemAction.SystemActionId = SystemAction.Id");
sql.AppendLine($"   Left Join ActionLocation on BfsComponentSystemAction.ActionLocationId = ActionLocation.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsComponentSystemActionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsComponentSystemAction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("BfsComponentSystemAction.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (filter.BfsComponentId.HasValue)
                {
                    sql.AppendLine("BfsComponentSystemAction.BfsComponentId = @BfsComponentId");
                    parameters.Add("@BfsComponentId", filter.BfsComponentId.Value);
                }
if (filter.SystemActionId.HasValue)
                {
                    sql.AppendLine("BfsComponentSystemAction.SystemActionId = @SystemActionId");
                    parameters.Add("@SystemActionId", filter.SystemActionId.Value);
                }
if (filter.ActionLocationId.HasValue)
                {
                    sql.AppendLine("BfsComponentSystemAction.ActionLocationId = @ActionLocationId");
                    parameters.Add("@ActionLocationId", filter.ActionLocationId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsComponentSystemActionListFilter> request, DynamicParameters parameters)
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