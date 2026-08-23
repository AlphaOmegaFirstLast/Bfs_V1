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
    public class CurrentPriceList: QueryBase<CurrentPriceListFilter>,  ICurrentPriceList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public CurrentPriceList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<CurrentPriceListItem>> GetAsync(QueryRequest<CurrentPriceListFilter> request)
        {
            var response = new QueryResponse<CurrentPriceListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<CurrentPriceListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<CurrentPriceListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "CurrentPrice", FieldName = "Id", DbName = "stkxCurrentPrice.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CurrentPrice", FieldName = "StockShareId", DbName = "stkxCurrentPrice.StockShareId", QueryName = "StockShareId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CurrentPrice", FieldName = "TransactionDate", DbName = "stkxCurrentPrice.TransactionDate", QueryName = "TransactionDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CurrentPrice", FieldName = "Price", DbName = "stkxCurrentPrice.Price", QueryName = "Price", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "StockShare", FieldName = "Name", DbName = "stkxStockShare.Name", QueryName = "StockShareName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxCurrentPrice ");

           sql.AppendLine($"   Left Join stkxStockShare on stkxCurrentPrice.StockShareId = stkxStockShare.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<CurrentPriceListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxCurrentPrice.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxCurrentPrice.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxCurrentPrice.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.StockShareId.HasValue)
                {
                    sql.AppendLine("stkxCurrentPrice.StockShareId = @StockShareId");
                    parameters.Add("@StockShareId", filter.StockShareId.Value);
                }

                if (filter.TransactionDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCurrentPrice.TransactionDate >= @TransactionDateFrom");
                    parameters.Add("@TransactionDateFrom", filter.TransactionDate.From.Value);
                }
                if (filter.TransactionDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCurrentPrice.TransactionDate <= @TransactionDateTo");
                    parameters.Add("@TransactionDateTo", filter.TransactionDate.To.Value);
                }
if (filter.Price?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCurrentPrice.Price >= @PriceFrom");
                    parameters.Add("@PriceFrom", filter.Price.From.Value);
                }
                if (filter.Price?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCurrentPrice.Price <= @PriceTo");
                    parameters.Add("@PriceTo", filter.Price.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<CurrentPriceListFilter> request, DynamicParameters parameters)
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