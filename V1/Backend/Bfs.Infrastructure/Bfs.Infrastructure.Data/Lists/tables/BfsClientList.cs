using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class BfsClientList: QueryBase<BfsClientListFilter>,  IBfsClientList
    {
        public BfsClientList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsClientListItem>> GetAsync(QueryRequest<BfsClientListFilter> request)
        {
            var response = new QueryResponse<BfsClientListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsClientListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BfsClientListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "BfsClient.DbConnection", QueryName = "BfsClientDbConnection", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsClient.Id", QueryName = "BfsClientId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsClient.Name", QueryName = "BfsClientName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsClient.Notes", QueryName = "BfsClientNotes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsClient.CustomFields", QueryName = "BfsClientCustomFields", IsAggregare = false });

            //lookups

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsClient ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsClientListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsClient.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("BfsClient.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsClientListFilter> request, DynamicParameters parameters)
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