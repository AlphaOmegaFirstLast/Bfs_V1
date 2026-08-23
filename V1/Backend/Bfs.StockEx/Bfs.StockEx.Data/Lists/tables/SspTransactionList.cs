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
    public class SspTransactionList: QueryBase<SspTransactionListFilter>,  ISspTransactionList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public SspTransactionList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<SspTransactionListItem>> GetAsync(QueryRequest<SspTransactionListFilter> request)
        {
            var response = new QueryResponse<SspTransactionListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<SspTransactionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<SspTransactionListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "Id", DbName = "stkxSspTransaction.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "Name", DbName = "stkxSspTransaction.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "Notes", DbName = "stkxSspTransaction.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "SourceDate", DbName = "stkxSspTransaction.SourceDate", QueryName = "SourceDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "TransactionDate", DbName = "stkxSspTransaction.TransactionDate", QueryName = "TransactionDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "Source", DbName = "stkxSspTransaction.Source", QueryName = "Source", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "SsPortfolioId", DbName = "stkxSspTransaction.SsPortfolioId", QueryName = "SsPortfolioId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "TransactionTypeId", DbName = "stkxSspTransaction.TransactionTypeId", QueryName = "TransactionTypeId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "Quantity", DbName = "stkxSspTransaction.Quantity", QueryName = "Quantity", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "Price", DbName = "stkxSspTransaction.Price", QueryName = "Price", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "StockShareId", DbName = "stkxSspTransaction.StockShareId", QueryName = "StockShareId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "ToQuantity", DbName = "stkxSspTransaction.ToQuantity", QueryName = "ToQuantity", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "ToPortfolioId", DbName = "stkxSspTransaction.ToPortfolioId", QueryName = "ToPortfolioId", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Name", DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "TransactionType", FieldName = "Name", DbName = "stkxTransactionType.Name", QueryName = "TransactionTypeName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "StockShare", FieldName = "Name", DbName = "stkxStockShare.Name", QueryName = "StockShareName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ToPortfolio", FieldName = "Name", DbName = "asPortfolio.Name", QueryName = "ToPortfolioName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxSspTransaction ");

           sql.AppendLine($"   Left Join stkxSsPortfolio on stkxSspTransaction.SsPortfolioId = stkxSsPortfolio.Id");
sql.AppendLine($"   Left Join stkxTransactionType on stkxSspTransaction.TransactionTypeId = stkxTransactionType.Id");
sql.AppendLine($"   Left Join stkxStockShare on stkxSspTransaction.StockShareId = stkxStockShare.Id");
sql.AppendLine($"   Left Join stkxSsPortfolio  asPortfolio on stkxSspTransaction.ToPortfolioId = asPortfolio.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<SspTransactionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxSspTransaction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxSspTransaction.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxSspTransaction.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.SsPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxSspTransaction.SsPortfolioId = @SsPortfolioId");
                    parameters.Add("@SsPortfolioId", filter.SsPortfolioId.Value);
                }
if (filter.TransactionTypeId.HasValue)
                {
                    sql.AppendLine("stkxSspTransaction.TransactionTypeId = @TransactionTypeId");
                    parameters.Add("@TransactionTypeId", filter.TransactionTypeId.Value);
                }
if (filter.StockShareId.HasValue)
                {
                    sql.AppendLine("stkxSspTransaction.StockShareId = @StockShareId");
                    parameters.Add("@StockShareId", filter.StockShareId.Value);
                }
if (filter.ToPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxSspTransaction.ToPortfolioId = @ToPortfolioId");
                    parameters.Add("@ToPortfolioId", filter.ToPortfolioId.Value);
                }

                if (filter.SourceDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.SourceDate >= @SourceDateFrom");
                    parameters.Add("@SourceDateFrom", filter.SourceDate.From.Value);
                }
                if (filter.SourceDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.SourceDate <= @SourceDateTo");
                    parameters.Add("@SourceDateTo", filter.SourceDate.To.Value);
                }
if (filter.TransactionDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.TransactionDate >= @TransactionDateFrom");
                    parameters.Add("@TransactionDateFrom", filter.TransactionDate.From.Value);
                }
                if (filter.TransactionDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.TransactionDate <= @TransactionDateTo");
                    parameters.Add("@TransactionDateTo", filter.TransactionDate.To.Value);
                }
if (filter.Quantity?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Quantity >= @QuantityFrom");
                    parameters.Add("@QuantityFrom", filter.Quantity.From.Value);
                }
                if (filter.Quantity?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Quantity <= @QuantityTo");
                    parameters.Add("@QuantityTo", filter.Quantity.To.Value);
                }
if (filter.Price?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Price >= @PriceFrom");
                    parameters.Add("@PriceFrom", filter.Price.From.Value);
                }
                if (filter.Price?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Price <= @PriceTo");
                    parameters.Add("@PriceTo", filter.Price.To.Value);
                }
if (filter.ToQuantity?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.ToQuantity >= @ToQuantityFrom");
                    parameters.Add("@ToQuantityFrom", filter.ToQuantity.From.Value);
                }
                if (filter.ToQuantity?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.ToQuantity <= @ToQuantityTo");
                    parameters.Add("@ToQuantityTo", filter.ToQuantity.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<SspTransactionListFilter> request, DynamicParameters parameters)
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

