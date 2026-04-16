using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data;
using System.Text;

namespace Bfs.Master.Data.Lists
{
    public class CustomReportsList: QueryBase<CustomReportsListFilter>,  ICustomReportsList
    {
        public CustomReportsList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<CustomReportsListItem>> GetAsync(QueryRequest<CustomReportsListFilter> request)
        {
            var response = new QueryResponse<CustomReportsListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<CustomReportsListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<CustomReportsListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "CustomReports.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomReports.Name", QueryName = "Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomReports.Request", QueryName = "Request", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomReports.BaseReport", QueryName = "BaseReport", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomReports.IsPrivate", QueryName = "IsPrivate", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomReports.CreatedBy", QueryName = "CreatedBy", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomReports.Url", QueryName = "Url", IsAggregare = false });

            //lookups

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From CustomReports ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<CustomReportsListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" CustomReports.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("CustomReports.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<CustomReportsListFilter> request, DynamicParameters parameters)
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