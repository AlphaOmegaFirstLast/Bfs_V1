using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class BfsTenantList: QueryBase<BfsTenantListFilter>,  IBfsTenantList
    {
        public BfsTenantList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsTenantListItem>> GetAsync(QueryRequest<BfsTenantListFilter> request)
        {
            var response = new QueryResponse<BfsTenantListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsTenantListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BfsTenantListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "BfsTenant.DbConnection", QueryName = "DbConnection", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsTenant.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsTenant.Logo", QueryName = "Logo", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsTenant.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsTenant.CustomFields", QueryName = "CustomFields", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsTenant.Name", QueryName = "Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsTenant.CompanyName", QueryName = "CompanyName", IsAggregare = false });

            //lookups

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsTenant ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsTenantListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsTenant.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.Logo))
                {
                    sql.AppendLine("BfsTenant.Logo like '%'+@Logo+'%' ");
                    parameters.Add("@Logo", filter.Logo);
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("BfsTenant.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }
if (!string.IsNullOrEmpty(filter.CompanyName))
                {
                    sql.AppendLine("BfsTenant.CompanyName like '%'+@CompanyName+'%' ");
                    parameters.Add("@CompanyName", filter.CompanyName);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsTenantListFilter> request, DynamicParameters parameters)
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
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

