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
    public class StockShareList: QueryBase<StockShareListFilter>,  IStockShareList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public StockShareList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<StockShareListItem>> GetAsync(QueryRequest<StockShareListFilter> request)
        {
            var response = new QueryResponse<StockShareListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<StockShareListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<StockShareListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "StockShare", FieldName = "Id", DbName = "stkxStockShare.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "StockShare", FieldName = "Name", DbName = "stkxStockShare.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "StockShare", FieldName = "Notes", DbName = "stkxStockShare.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "StockShare", FieldName = "TradingRoomId", DbName = "stkxStockShare.TradingRoomId", QueryName = "TradingRoomId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "StockShare", FieldName = "CurrencyId", DbName = "stkxStockShare.CurrencyId", QueryName = "CurrencyId", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "TradingRoom", FieldName = "Name", DbName = "stkxTradingRoom.Name", QueryName = "TradingRoomName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Currency", FieldName = "Name", DbName = "stkxCurrency.Name", QueryName = "CurrencyName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxStockShare ");

           sql.AppendLine($"   Left Join stkxTradingRoom on stkxStockShare.TradingRoomId = stkxTradingRoom.Id");
sql.AppendLine($"   Left Join stkxCurrency on stkxStockShare.CurrencyId = stkxCurrency.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<StockShareListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxStockShare.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxStockShare.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxStockShare.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.TradingRoomId.HasValue)
                {
                    sql.AppendLine("stkxStockShare.TradingRoomId = @TradingRoomId");
                    parameters.Add("@TradingRoomId", filter.TradingRoomId.Value);
                }
if (filter.CurrencyId.HasValue)
                {
                    sql.AppendLine("stkxStockShare.CurrencyId = @CurrencyId");
                    parameters.Add("@CurrencyId", filter.CurrencyId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<StockShareListFilter> request, DynamicParameters parameters)
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