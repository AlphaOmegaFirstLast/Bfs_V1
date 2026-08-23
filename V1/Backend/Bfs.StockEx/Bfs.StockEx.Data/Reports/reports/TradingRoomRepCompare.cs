using Bfs.Core.Data;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data;
using Bfs.StockEx.Data.Interfaces;
using System.Text;

namespace Bfs.StockEx.Data.Reports
{
    public class TradingRoomRepCompare :QueryBase<TradingRoomRepCompareFilter>,  ITradingRoomRepCompare
    {
        private readonly IResourceSecurity _resourceSecurity;

        public TradingRoomRepCompare(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<TradingRoomRepCompareItem>> GetAsync(QueryRequest<TradingRoomRepCompareFilter> request)
        {
            var response = new QueryResponse<TradingRoomRepCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<TradingRoomRepCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<TradingRoomRepCompareItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "stkxTradingRoom.Id", QueryName = "TradingRoom_Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxTradingRoom.Name", QueryName = "TradingRoom_Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxTradingRoom.Notes", QueryName = "TradingRoom_Notes", IsAggregare = false });

            //lookups

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxTradingRoom ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<TradingRoomRepCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxTradingRoom.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.TradingRoom_Id.HasValue) && (filter.TradingRoom_Id>0))
                {
                    sql.AppendLine(" AND stkxTradingRoom.Id = @TradingRoom_Id");
                    parameters.Add("@TradingRoom_Id", filter.TradingRoom_Id);
                }

                if (!string.IsNullOrEmpty(filter.TradingRoom_Name))
                {
                    sql.AppendLine("stkxTradingRoom.Name like '%'+@TradingRoom_Name+'%' ");
                    parameters.Add("@TradingRoom_Name", filter.TradingRoom_Name);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<TradingRoomRepCompareFilter> request, DynamicParameters parameters)
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

