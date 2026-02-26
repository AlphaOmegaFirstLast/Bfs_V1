using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class BfsClientSystemList: QueryBase<BfsClientSystemListFilter>,  IBfsClientSystemList
    {
        public BfsClientSystemList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsClientSystemListItem>> GetAsync(QueryRequest<BfsClientSystemListFilter> request)
        {
            var response = new QueryResponse<BfsClientSystemListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsClientSystemListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BfsClientSystemListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "BfsClientSystem.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsClientSystem.BfsClientId", QueryName = "BfsClientId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsClientSystem.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "BfsClient.Name", QueryName = "BfsClientName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsClientSystem ");

           sql.AppendLine($"   Left Join BfsClient on BfsClientSystem.BfsClientId = BfsClient.Id");
sql.AppendLine($"   Left Join BfsSystem on BfsClientSystem.BfsSystemId = BfsSystem.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsClientSystemListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsClientSystem.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (filter.BfsClientId.HasValue)
                {
                    sql.AppendLine("BfsClientSystem.BfsClientId = @BfsClientId");
                    parameters.Add("@BfsClientId", filter.BfsClientId.Value);
                }
if (filter.BfsSystemId.HasValue)
                {
                    sql.AppendLine("BfsClientSystem.BfsSystemId = @BfsSystemId");
                    parameters.Add("@BfsSystemId", filter.BfsSystemId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsClientSystemListFilter> request, DynamicParameters parameters)
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

