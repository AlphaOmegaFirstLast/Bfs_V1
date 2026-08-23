using Bfs.Core.Data;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data;
using System.Text;

namespace Bfs.Master.Data.Lists
{
    public class BusinessActionList: QueryBase<BusinessActionListFilter>,  IBusinessActionList
    {
        public BusinessActionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BusinessActionListItem>> GetAsync(QueryRequest<BusinessActionListFilter> request)
        {
            var response = new QueryResponse<BusinessActionListItem>();

            await SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BusinessActionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BusinessActionListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "BusinessAction", FieldName = "Id", DbName = "BusinessAction.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BusinessAction", FieldName = "ShortName", DbName = "BusinessAction.ShortName", QueryName = "ShortName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BusinessAction", FieldName = "ActionTypeId", DbName = "BusinessAction.ActionTypeId", QueryName = "ActionTypeId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BusinessAction", FieldName = "WriterTypeId", DbName = "BusinessAction.WriterTypeId", QueryName = "WriterTypeId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BusinessAction", FieldName = "MatchProperty", DbName = "BusinessAction.MatchProperty", QueryName = "MatchProperty", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BusinessAction", FieldName = "MatchValues", DbName = "BusinessAction.MatchValues", QueryName = "MatchValues", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BusinessAction", FieldName = "ActionTemplate", DbName = "BusinessAction.ActionTemplate", QueryName = "ActionTemplate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BusinessAction", FieldName = "Name", DbName = "BusinessAction.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BusinessAction", FieldName = "Notes", DbName = "BusinessAction.Notes", QueryName = "Notes", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "ActionType", FieldName = "Name", DbName = "ActionType.Name", QueryName = "ActionTypeName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "WriterType", FieldName = "Name", DbName = "WriterType.Name", QueryName = "WriterTypeName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BusinessAction ");

           sql.AppendLine($"   Left Join ActionType on BusinessAction.ActionTypeId = ActionType.Id");
sql.AppendLine($"   Left Join WriterType on BusinessAction.WriterTypeId = WriterType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BusinessActionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BusinessAction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("BusinessAction.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.ShortName))
                {
                    sql.AppendLine("BusinessAction.ShortName like '%'+@ShortName+'%' ");
                    parameters.Add("@ShortName", filter.ShortName);
                }
if (!string.IsNullOrEmpty(filter.MatchProperty))
                {
                    sql.AppendLine("BusinessAction.MatchProperty like '%'+@MatchProperty+'%' ");
                    parameters.Add("@MatchProperty", filter.MatchProperty);
                }
if (!string.IsNullOrEmpty(filter.MatchValues))
                {
                    sql.AppendLine("BusinessAction.MatchValues like '%'+@MatchValues+'%' ");
                    parameters.Add("@MatchValues", filter.MatchValues);
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("BusinessAction.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.ActionTypeId.HasValue)
                {
                    sql.AppendLine("BusinessAction.ActionTypeId = @ActionTypeId");
                    parameters.Add("@ActionTypeId", filter.ActionTypeId.Value);
                }
if (filter.WriterTypeId.HasValue)
                {
                    sql.AppendLine("BusinessAction.WriterTypeId = @WriterTypeId");
                    parameters.Add("@WriterTypeId", filter.WriterTypeId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BusinessActionListFilter> request, DynamicParameters parameters)
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

