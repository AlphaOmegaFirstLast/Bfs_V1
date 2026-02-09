using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class BfsComponentSystemActionList: QueryBase<BfsComponentSystemActionListFilter>,  IBfsComponentSystemActionList
    {
        public BfsComponentSystemActionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsComponentSystemActionListItem>> GetAsync(QueryRequest<BfsComponentSystemActionListFilter> request)
        {
            var response = new QueryResponse<BfsComponentSystemActionListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsComponentSystemActionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BfsComponentSystemActionListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "BfsComponentSystemAction.Id", QueryName = "BfsComponentSystemActionId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponentSystemAction.BfsComponentId", QueryName = "BfsComponentSystemActionBfsComponentId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponentSystemAction.SystemActionId", QueryName = "BfsComponentSystemActionSystemActionId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponentSystemAction.ActionLocationId", QueryName = "BfsComponentSystemActionActionLocationId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.Name", QueryName = "SystemActionName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "ActionLocation.Name", QueryName = "ActionLocationName", IsAggregare = false });

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
    }
}