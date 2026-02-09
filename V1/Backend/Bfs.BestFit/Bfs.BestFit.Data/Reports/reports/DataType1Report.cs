using Bfs.Core.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.BestFit.Data;
using Bfs.BestFit.Data.Interfaces;
using System.Text;

namespace Bfs.BestFit.Data.Reports
{
    public class DataType1Report : QueryBase<DataType1Filter>, IDataType1Report
    {
        public DataType1Report(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<DataType1Item>> GetAsync(QueryRequest<DataType1Filter> request)
        {
            var response = new QueryResponse<DataType1Item>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<DataType1Item>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<DataType1Item>)items;

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
            _fieldList.Add(new QueryField() { DbName = "DataType.Id", QueryName = "DataTypeId", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "DataType.Name", QueryName = "DataTypeName", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "DataType.Notes", QueryName = "DataTypeNotes", IsAggregare = false });

            //lookups

            //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
            var sql = new StringBuilder();
            sql.AppendLine(" From DataType ");

            return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<DataType1Filter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            var sql = new StringBuilder();
            sql.AppendLine(" DataType.isDeleted=0 ");

            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.DataTypeNotes))
                {
                    sql.AppendLine("DataType.Notes like '%'+@DataTypeNotes+'%' ");
                    parameters.Add("@DataTypeNotes", filter.DataTypeNotes);
                }

            }

            if (filter.DataTypeId?.From.HasValue == true)
            {
                sql.AppendLine("DataType.Id >= @DataTypeIdFrom");
                parameters.Add("@DataTypeIdFrom", filter.DataTypeId.From.Value);
            }
            if (filter.DataTypeId?.To.HasValue == true)
            {
                sql.AppendLine("DataType.Id <= @DataTypeIdTo");
                parameters.Add("@DataTypeIdTo", filter.DataTypeId.To.Value);
            }

            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));
        }

        protected override string GetHavingConditions(QueryRequest<DataType1Filter> request, DynamicParameters parameters)
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