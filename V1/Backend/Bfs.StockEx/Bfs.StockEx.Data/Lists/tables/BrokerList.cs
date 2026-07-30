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
    public class BrokerList: QueryBase<BrokerListFilter>,  IBrokerList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public BrokerList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BrokerListItem>> GetAsync(QueryRequest<BrokerListFilter> request)
        {
            var response = new QueryResponse<BrokerListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BrokerListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BrokerListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "Broker", FieldName = "Id", DbName = "stkxBroker.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Broker", FieldName = "IsDeleted", DbName = "stkxBroker.IsDeleted", QueryName = "IsDeleted", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Broker", FieldName = "Code", DbName = "stkxBroker.Code", QueryName = "Code", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Broker", FieldName = "Name", DbName = "stkx(Broker.FirstName + ' ' + Broker.LastName) ", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Broker", FieldName = "TradingRoomId", DbName = "stkxBroker.TradingRoomId", QueryName = "TradingRoomId", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "TradingRoom", FieldName = "Name", DbName = "stkxTradingRoom.Name", QueryName = "TradingRoomName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxBroker ");

           sql.AppendLine($"   Left Join stkxTradingRoom on stkxBroker.TradingRoomId = stkxTradingRoom.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BrokerListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxBroker.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.Code))
                {
                    sql.AppendLine("stkxBroker.Code like '%'+@Code+'%' ");
                    parameters.Add("@Code", filter.Code);
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkx(Broker.FirstName + ' ' + Broker.LastName)  like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.TradingRoomId.HasValue)
                {
                    sql.AppendLine("stkxBroker.TradingRoomId = @TradingRoomId");
                    parameters.Add("@TradingRoomId", filter.TradingRoomId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BrokerListFilter> request, DynamicParameters parameters)
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