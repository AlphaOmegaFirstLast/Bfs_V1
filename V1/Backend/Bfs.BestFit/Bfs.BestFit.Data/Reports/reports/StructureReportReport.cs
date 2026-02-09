using Bfs.Core.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data;
using Bfs.BestFit.Data.Interfaces;
using System.Text;

namespace Bfs.BestFit.Data.Reports
{
    public class StructureReportReport :QueryBase<StructureReportFilter>,  IStructureReportReport
    {
        public StructureReportReport(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<StructureReportItem>> GetAsync(QueryRequest<StructureReportFilter> request)
        {
            var response = new QueryResponse<StructureReportItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<StructureReportItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<StructureReportItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "Component.DataTypeId", QueryName = "ComponentDataTypeId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "Component.DisplayName", QueryName = "ComponentDisplayName", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "DataType.Name", QueryName = "DataTypeName", IsAggregare = false });

           //Aggregates
           _fieldList.Add(new QueryField() { DbName = "Count(TableField.id)", QueryName = "countId", IsAggregare = true });

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From TableField ");

           sql.AppendLine($"   Left Join Component on TableField.ComponentId = Component.Id");

           sql.AppendLine($"   Left Join DataType on Component.DataTypeId = DataType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<StructureReportFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            var sql = new StringBuilder() ;
            sql.AppendLine(" TableField.isDeleted=0 ");

            if (filter != null)
            {

            if (!string.IsNullOrEmpty(filter.ComponentDisplayName))
            {
                sql.AppendLine("Component.DisplayName like '%'+@ComponentDisplayName+'%' ");
                parameters.Add("@ComponentDisplayName", filter.ComponentDisplayName);
            }

            if (filter.ComponentDataTypeId.HasValue)
            {
                sql.AppendLine("Component.DataTypeId = @ComponentDataTypeId");
                parameters.Add("@ComponentDataTypeId", filter.ComponentDataTypeId.Value);
            }

            }

            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<StructureReportFilter> request, DynamicParameters parameters)
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