using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Lists
{
    public class CustomReportsList: QueryBase<CustomReportsListFilter>,  ICustomReportsList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public CustomReportsList(string connectionString, IResourceSecurity? resourceSecurity
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

        )
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

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
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<CustomReportsListItem> DoMapping(IEnumerable<CustomReportsListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (CustomReportsListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "Id", DbName = "CustomReports.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "Name", DbName = "CustomReports.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "Request", DbName = "CustomReports.Request", QueryName = "Request", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "BaseReport", DbName = "CustomReports.BaseReport", QueryName = "BaseReport", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "IsPrivate", DbName = "CustomReports.IsPrivate", QueryName = "IsPrivate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "CreatedBy", DbName = "CustomReports.CreatedBy", QueryName = "CreatedBy", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomReports", FieldName = "Url", DbName = "CustomReports.Url", QueryName = "Url", IsAggregare = false});

            //object fields

            //lookups

            //autoCompletes

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
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("CustomReports.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

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
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

    }
}