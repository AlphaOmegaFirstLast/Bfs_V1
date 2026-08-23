using Bfs.Core.Data;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data;
using System.Text;

namespace Bfs.StockEx.Data.Lists
{
    public class CustomReportsList: QueryBase<CustomReportsListFilter>,  ICustomReportsList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public CustomReportsList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<CustomReportsListItem>> GetAsync(QueryRequest<CustomReportsListFilter> request)
        {
            var response = new QueryResponse<CustomReportsListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<CustomReportsListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<CustomReportsListItem>)items;

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "Id", DbName = "stkxCustomReports.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "Name", DbName = "stkxCustomReports.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "Request", DbName = "stkxCustomReports.Request", QueryName = "Request", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "BaseReport", DbName = "stkxCustomReports.BaseReport", QueryName = "BaseReport", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "IsPrivate", DbName = "stkxCustomReports.IsPrivate", QueryName = "IsPrivate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "CreatedBy", DbName = "stkxCustomReports.CreatedBy", QueryName = "CreatedBy", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "Url", DbName = "stkxCustomReports.Url", QueryName = "Url", IsAggregare = false});

            //lookups

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxCustomReports ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<CustomReportsListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxCustomReports.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxCustomReports.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxCustomReports.Name like '%'+@Name+'%' ");
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