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
            _fieldList.Add(new QueryField() { DbName = "SystemAction.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.ShortName", QueryName = "ShortName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.ActionTypeId", QueryName = "ActionTypeId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.WriterTypeId", QueryName = "WriterTypeId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.MatchProperty", QueryName = "MatchProperty", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.MatchValues", QueryName = "MatchValues", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.ActionTemplate", QueryName = "ActionTemplate", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemAction.Name", QueryName = "Name", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "ActionType.Name", QueryName = "ActionTypeName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "WriterType.Name", QueryName = "WriterTypeName", IsAggregare = false });

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
    }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

