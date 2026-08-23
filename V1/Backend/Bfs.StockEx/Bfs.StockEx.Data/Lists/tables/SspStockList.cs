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
    public class SspStockList: QueryBase<SspStockListFilter>,  ISspStockList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public SspStockList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<SspStockListItem>> GetAsync(QueryRequest<SspStockListFilter> request)
        {
            var response = new QueryResponse<SspStockListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<SspStockListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<SspStockListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "SspStock", FieldName = "Id", DbName = "stkxSspStock.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspStock", FieldName = "Name", DbName = "stkxSspStock.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspStock", FieldName = "Notes", DbName = "stkxSspStock.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspStock", FieldName = "SsPortfolioId", DbName = "stkxSspStock.SsPortfolioId", QueryName = "SsPortfolioId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspStock", FieldName = "Quantity", DbName = "stkxSspStock.Quantity", QueryName = "Quantity", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspStock", FieldName = "StockShareId", DbName = "stkxSspStock.StockShareId", QueryName = "StockShareId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspStock", FieldName = "AverageCost", DbName = "stkxSspStock.AverageCost", QueryName = "AverageCost", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Name", DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "StockShare", FieldName = "Name", DbName = "stkxStockShare.Name", QueryName = "StockShareName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxSspStock ");

           sql.AppendLine($"   Left Join stkxSsPortfolio on stkxSspStock.SsPortfolioId = stkxSsPortfolio.Id");
sql.AppendLine($"   Left Join stkxStockShare on stkxSspStock.StockShareId = stkxStockShare.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<SspStockListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxSspStock.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxSspStock.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxSspStock.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.SsPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxSspStock.SsPortfolioId = @SsPortfolioId");
                    parameters.Add("@SsPortfolioId", filter.SsPortfolioId.Value);
                }
if (filter.StockShareId.HasValue)
                {
                    sql.AppendLine("stkxSspStock.StockShareId = @StockShareId");
                    parameters.Add("@StockShareId", filter.StockShareId.Value);
                }

                if (filter.Quantity?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspStock.Quantity >= @QuantityFrom");
                    parameters.Add("@QuantityFrom", filter.Quantity.From.Value);
                }
                if (filter.Quantity?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspStock.Quantity <= @QuantityTo");
                    parameters.Add("@QuantityTo", filter.Quantity.To.Value);
                }
if (filter.AverageCost?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspStock.AverageCost >= @AverageCostFrom");
                    parameters.Add("@AverageCostFrom", filter.AverageCost.From.Value);
                }
                if (filter.AverageCost?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspStock.AverageCost <= @AverageCostTo");
                    parameters.Add("@AverageCostTo", filter.AverageCost.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<SspStockListFilter> request, DynamicParameters parameters)
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

