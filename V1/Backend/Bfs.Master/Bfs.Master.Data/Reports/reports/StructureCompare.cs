using Bfs.Core.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Master.Data;
using Bfs.Master.Data.Interfaces;
using System.Text;

namespace Bfs.Master.Data.Reports
{
    public class StructureCompare :QueryBase<StructureCompareFilter>,  IStructureCompare
    {
        public StructureCompare(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<StructureCompareItem>> GetAsync(QueryRequest<StructureCompareFilter> request)
        {
            var response = new QueryResponse<StructureCompareItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<StructureCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<StructureCompareItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "BfsComponent.DataTypeId", QueryName = "BfsComponent_DataTypeId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.DisplayName", QueryName = "BfsComponent_DisplayName", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "DataType.Name", QueryName = "DataTypeName", IsAggregare = false });

           //Aggregates
           _fieldList.Add(new QueryField() { DbName = "Count(BfsField.id)", QueryName = "countId", IsAggregare = true });

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsField ");

           sql.AppendLine($"   Left Join BfsComponent on BfsField.BfsComponentId = BfsComponent.Id");

           sql.AppendLine($"   Left Join DataType on BfsComponent.DataTypeId = DataType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<StructureCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsField.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.DisplayName))
                {
                    sql.AppendLine("BfsComponent.DisplayName like '%'+@DisplayName+'%' ");
                    parameters.Add("@DisplayName", filter.DisplayName);
                }

                if (filter.DataTypeId.HasValue)
                {
                    sql.AppendLine("BfsComponent.DataTypeId = @DataTypeId");
                    parameters.Add("@DataTypeId", filter.DataTypeId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<StructureCompareFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            if (filter.countId?.From.HasValue == true)
            {
                sql.AppendLine("Count(BfsComponent.Id) >= @countIdFrom");
                parameters.Add("@countIdFrom", filter.countId.From.Value);
            }
            if (filter.countId?.To.HasValue == true)
            {
                sql.AppendLine("Count(BfsComponent.Id) <= @countIdTo");
                parameters.Add("@countIdTo", filter.countId.To.Value);
            }

            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
       }       
    }
}