using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
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

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BusinessActionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BusinessActionListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "BusinessAction.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BusinessAction.Name", QueryName = "Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BusinessAction.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BusinessAction.ActionTypeId", QueryName = "ActionTypeId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "ActionType.Name", QueryName = "ActionTypeName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BusinessAction ");

           sql.AppendLine($"   Left Join ActionType on BusinessAction.ActionTypeId = ActionType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BusinessActionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BusinessAction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

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