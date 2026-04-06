using Bfs.Core.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data;
using Bfs.Auth.Data.Interfaces;
using System.Text;

namespace Bfs.Auth.Data.Reports
{
    public class RoleRepCompare :QueryBase<RoleRepCompareFilter>,  IRoleRepCompare
    {
        public RoleRepCompare(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<RoleRepCompareItem>> GetAsync(QueryRequest<RoleRepCompareFilter> request)
        {
            var response = new QueryResponse<RoleRepCompareItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<RoleRepCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<RoleRepCompareItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "athAuthRole.Id", QueryName = "AuthRole_Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athAuthRole.Name", QueryName = "AuthRole_Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athAuthRole.Notes", QueryName = "AuthRole_Notes", IsAggregare = false });

            //lookups

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From [DbParentTable] ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<RoleRepCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" [DbParentTable].isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("athAuthRole.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<RoleRepCompareFilter> request, DynamicParameters parameters)
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

