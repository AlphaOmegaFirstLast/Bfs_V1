using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class BfsSystemList: QueryBase<BfsSystemListFilter>,  IBfsSystemList
    {
        public BfsSystemList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsSystemListItem>> GetAsync(QueryRequest<BfsSystemListFilter> request)
        {
            var response = new QueryResponse<BfsSystemListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsSystemListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BfsSystemListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "BfsSystem.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsSystem.Name", QueryName = "Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsSystem.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsSystem.BfsClientId", QueryName = "BfsClientId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsSystem.SystemTemplateId", QueryName = "SystemTemplateId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsSystem.BasePortNumber", QueryName = "BasePortNumber", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsSystem.DbPrefix", QueryName = "DbPrefix", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "BfsClient.Name", QueryName = "BfsClientName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "SystemTemplate.Name", QueryName = "SystemTemplateName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsSystem ");

           sql.AppendLine($"   Left Join BfsClient on BfsSystem.BfsClientId = BfsClient.Id");
sql.AppendLine($"   Left Join SystemTemplate on BfsSystem.SystemTemplateId = SystemTemplate.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsSystemListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsSystem.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("BfsSystem.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.BfsClientId.HasValue)
                {
                    sql.AppendLine("BfsSystem.BfsClientId = @BfsClientId");
                    parameters.Add("@BfsClientId", filter.BfsClientId.Value);
                }
if (filter.SystemTemplateId.HasValue)
                {
                    sql.AppendLine("BfsSystem.SystemTemplateId = @SystemTemplateId");
                    parameters.Add("@SystemTemplateId", filter.SystemTemplateId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsSystemListFilter> request, DynamicParameters parameters)
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