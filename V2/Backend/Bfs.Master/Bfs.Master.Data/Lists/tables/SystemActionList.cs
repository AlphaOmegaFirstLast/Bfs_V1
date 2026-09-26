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
    public class SystemActionList: QueryBase<SystemActionListFilter>,  ISystemActionList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public SystemActionList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<SystemActionListItem>> GetAsync(QueryRequest<SystemActionListFilter> request)
        {
            var response = new QueryResponse<SystemActionListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<SystemActionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<SystemActionListItem> DoMapping(IEnumerable<SystemActionListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (SystemActionListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "Id", DbName = "SystemAction.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "ShortName", DbName = "SystemAction.ShortName", QueryName = "ShortName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "ActionTypeId", DbName = "SystemAction.ActionTypeId", QueryName = "ActionTypeId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "WriterTypeId", DbName = "SystemAction.WriterTypeId", QueryName = "WriterTypeId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "MatchProperty", DbName = "SystemAction.MatchProperty", QueryName = "MatchProperty", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "MatchValues", DbName = "SystemAction.MatchValues", QueryName = "MatchValues", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "ActionTemplate", DbName = "SystemAction.ActionTemplate", QueryName = "ActionTemplate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "Name", DbName = "SystemAction.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SystemAction", FieldName = "Notes", DbName = "SystemAction.Notes", QueryName = "Notes", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "ActionType", FieldName = "Name", DbName = "ActionType.Name", QueryName = "ActionTypeName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "WriterType", FieldName = "Name", DbName = "WriterType.Name", QueryName = "WriterTypeName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From SystemAction ");

           sql.AppendLine($"   Left Join ActionType on SystemAction.ActionTypeId = ActionType.Id");
sql.AppendLine($"   Left Join WriterType on SystemAction.WriterTypeId = WriterType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<SystemActionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" SystemAction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("SystemAction.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.ShortName))
                {
                    sql.AppendLine("SystemAction.ShortName like '%'+@ShortName+'%' ");
                    parameters.Add("@ShortName", filter.ShortName);
                }
if (!string.IsNullOrEmpty(filter.MatchProperty))
                {
                    sql.AppendLine("SystemAction.MatchProperty like '%'+@MatchProperty+'%' ");
                    parameters.Add("@MatchProperty", filter.MatchProperty);
                }
if (!string.IsNullOrEmpty(filter.MatchValues))
                {
                    sql.AppendLine("SystemAction.MatchValues like '%'+@MatchValues+'%' ");
                    parameters.Add("@MatchValues", filter.MatchValues);
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("SystemAction.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.ActionTypeId.HasValue)
                {
                    sql.AppendLine("SystemAction.ActionTypeId = @ActionTypeId");
                    parameters.Add("@ActionTypeId", filter.ActionTypeId.Value);
                }
if (filter.WriterTypeId.HasValue)
                {
                    sql.AppendLine("SystemAction.WriterTypeId = @WriterTypeId");
                    parameters.Add("@WriterTypeId", filter.WriterTypeId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<SystemActionListFilter> request, DynamicParameters parameters)
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