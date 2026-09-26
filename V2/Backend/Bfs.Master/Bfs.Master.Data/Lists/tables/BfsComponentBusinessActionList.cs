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
    public class BfsComponentBusinessActionList: QueryBase<BfsComponentBusinessActionListFilter>,  IBfsComponentBusinessActionList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public BfsComponentBusinessActionList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<BfsComponentBusinessActionListItem>> GetAsync(QueryRequest<BfsComponentBusinessActionListFilter> request)
        {
            var response = new QueryResponse<BfsComponentBusinessActionListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsComponentBusinessActionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<BfsComponentBusinessActionListItem> DoMapping(IEnumerable<BfsComponentBusinessActionListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (BfsComponentBusinessActionListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "BfsComponentBusinessAction", FieldName = "Id", DbName = "BfsComponentBusinessAction.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponentBusinessAction", FieldName = "BfsComponentId", DbName = "BfsComponentBusinessAction.BfsComponentId", QueryName = "BfsComponentId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponentBusinessAction", FieldName = "BusinessActionId", DbName = "BfsComponentBusinessAction.BusinessActionId", QueryName = "BusinessActionId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponentBusinessAction", FieldName = "ActionLocationId", DbName = "BfsComponentBusinessAction.ActionLocationId", QueryName = "ActionLocationId", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "Name", DbName = "BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BusinessAction", FieldName = "Name", DbName = "BusinessAction.Name", QueryName = "BusinessActionName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ActionLocation", FieldName = "Name", DbName = "ActionLocation.Name", QueryName = "ActionLocationName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsComponentBusinessAction ");

           sql.AppendLine($"   Left Join BfsComponent on BfsComponentBusinessAction.BfsComponentId = BfsComponent.Id");
sql.AppendLine($"   Left Join BusinessAction on BfsComponentBusinessAction.BusinessActionId = BusinessAction.Id");
sql.AppendLine($"   Left Join ActionLocation on BfsComponentBusinessAction.ActionLocationId = ActionLocation.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsComponentBusinessActionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsComponentBusinessAction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("BfsComponentBusinessAction.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (filter.BfsComponentId.HasValue)
                {
                    sql.AppendLine("BfsComponentBusinessAction.BfsComponentId = @BfsComponentId");
                    parameters.Add("@BfsComponentId", filter.BfsComponentId.Value);
                }
if (filter.BusinessActionId.HasValue)
                {
                    sql.AppendLine("BfsComponentBusinessAction.BusinessActionId = @BusinessActionId");
                    parameters.Add("@BusinessActionId", filter.BusinessActionId.Value);
                }
if (filter.ActionLocationId.HasValue)
                {
                    sql.AppendLine("BfsComponentBusinessAction.ActionLocationId = @ActionLocationId");
                    parameters.Add("@ActionLocationId", filter.ActionLocationId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsComponentBusinessActionListFilter> request, DynamicParameters parameters)
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