using Bfs.Core.Data;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data;
using Bfs.StockEx.Data.Interfaces;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Reports
{
    public class StockshareBrokerRepCompare :QueryBase<StockshareBrokerRepCompareFilter>,  IStockshareBrokerRepCompare
    {
        private readonly IResourceSecurity _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public StockshareBrokerRepCompare(string connectionString, IResourceSecurity resourceSecurity
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

        public async Task<QueryResponse<StockshareBrokerRepCompareItem>> GetAsync(QueryRequest<StockshareBrokerRepCompareFilter> request)
        {
            var response = new QueryResponse<StockshareBrokerRepCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<StockshareBrokerRepCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<StockshareBrokerRepCompareItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "stkxStockshare.Id", QueryName = "Stockshare_Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxStockshare.Name", QueryName = "Stockshare_Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxStockShare.TradingRoomId", QueryName = "StockShare_TradingRoomId", IsAggregare = false });
//Template_Start_Generated_Ignored_1
/*
            _fieldList.Add(new QueryField() { DbName = "stkxTradingRoom.BrokerId", QueryName = "TradingRoom_BrokerId", IsAggregare = false });
*/
            //Template_End_Generated_Ignored_1

            //lookups
            _fieldList.Add(new QueryField() { DbName = "stkxTradingRoom.Name", QueryName = "TradingRoomName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxBroker.Name", QueryName = "BrokerName", IsAggregare = false });

            //autoComplete

           //Aggregates

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxStockshare ");

           sql.AppendLine($"   Left Join stkxTradingRoom on stkxStockShare.TradingRoomId = stkxTradingRoom.Id");

            //Template_Start_Code_DontOverwrite_6
            sql.AppendLine($"   Left Join stkxBroker on stkxTradingRoom.Id = stkxBroker.TradingRoomId ");

            //Template_End_Code_DontOverwrite_6

            return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<StockshareBrokerRepCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxStockshare.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Stockshare_Id.HasValue) && (filter.Stockshare_Id>0))
                {
                    sql.AppendLine(" AND stkxStockshare.Id = @Stockshare_Id");
                    parameters.Add("@Stockshare_Id", filter.Stockshare_Id);
                }

                if (!string.IsNullOrEmpty(filter.Stockshare_Name))
                {
                    sql.AppendLine("stkxStockshare.Name like '%'+@Stockshare_Name+'%' ");
                    parameters.Add("@Stockshare_Name", filter.Stockshare_Name);
                }

                if (filter.StockShare_TradingRoomId.HasValue)
                {
                    sql.AppendLine("stkxStockShare.TradingRoomId = @StockShare_TradingRoomId");
                    parameters.Add("@StockShare_TradingRoomId", filter.StockShare_TradingRoomId.Value);
                }


                //Template_Start_Code_DontOverwrite_7
                if (filter.TradingRoom_BrokerId.HasValue)
                {
                    sql.AppendLine("stkxBroker.Id = @TradingRoom_BrokerId");
                    parameters.Add("@TradingRoom_BrokerId", filter.TradingRoom_BrokerId.Value);
                }
                //Template_End_Code_DontOverwrite_7      
            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<StockshareBrokerRepCompareFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            //Template_Start_Code_DontOverwrite_8
            //Template_End_Code_DontOverwrite_8    
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
       } 
//Template_Start_Code_DontOverwrite_9
//Template_End_Code_DontOverwrite_9   
    }
}