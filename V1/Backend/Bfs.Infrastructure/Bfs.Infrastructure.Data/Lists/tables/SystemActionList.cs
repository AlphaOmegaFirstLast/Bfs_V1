using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class SystemActionList: QueryBase<SystemActionListFilter>,  ISystemActionList
    {
        public SystemActionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<SystemActionListItem>> GetAsync(QueryRequest<SystemActionListFilter> request)
        {
            var response = new QueryResponse<SystemActionListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<SystemActionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<SystemActionListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "SystemAction.Id", QueryName = "SystemActionId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.Name", QueryName = "SystemActionName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.Notes", QueryName = "SystemActionNotes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.ActionTypeId", QueryName = "SystemActionActionTypeId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "ActionType.Name", QueryName = "ActionTypeName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From SystemAction ");

           sql.AppendLine($"   Left Join ActionType on SystemAction.ActionTypeId = ActionType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<SystemActionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" SystemAction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

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
    }
}