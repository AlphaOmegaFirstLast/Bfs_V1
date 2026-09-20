using Bfs.Core.Data;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data;
using Bfs.StockEx.Data.Interfaces;
using System.Text;

namespace Bfs.StockEx.Data.Reports
{
    public class ReportCompare : QueryBase<ReportCompareFilter>, IReportCompare
    {
        private readonly IResourceSecurity _resourceSecurity;

        public ReportCompare(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<ReportCompareItem>> GetAsync(QueryRequest<ReportCompareFilter> request)
        {
            var response = new QueryResponse<ReportCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<ReportCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<ReportCompareItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolio_Name", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxCurrency.Name", QueryName = "Currency_Name", IsAggregare = false });

            //lookups

            //autoComplete

            //Aggregates
            _fieldList.Add(new QueryField() { DbName = "Sum(stkxSspStock.Quantity * stkxCurrentPrice.Price)", QueryName = "sumStockValue", IsAggregare = true });
            _fieldList.Add(new QueryField() { DbName = "Sum(stkxSsPortfolioBalance.Balance)", QueryName = "sumCash", IsAggregare = true });

        }

        protected override string GetFromJoinStatement()
        {
            var sql = new StringBuilder();
            sql.AppendLine(" From stkxSsPortfolio ");

            sql.AppendLine($"   Left Join stkxSspStock on stkxSsPortfolio.Id = stkxSspStock.SsPortfolioId");
            sql.AppendLine($"   Left Join stkxStockShare on stkxSspStock.StockShareId = stkxStockShare.Id");
            sql.AppendLine($"   left join stkxCurrentPrice on stkxSspStock.StockShareId = stkxCurrentPrice.StockShareId");
            sql.AppendLine($"   left join stkxSsPortfolioBalance on stkxSsPortfolio.Id = stkxSsPortfolioBalance.SsPortfolioId");
            sql.AppendLine($"   left join stkxCurrency  on stkxCurrency.Id = stkxStockShare.CurrencyId");
            

            return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<ReportCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" stkxSsPortfolio.isDeleted=0 ");

            var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.SsPortfolio_Name))
                {
                    sql.AppendLine("stkxSsPortfolio.Name like '%'+@SsPortfolio_Name+'%' ");
                    parameters.Add("@SsPortfolio_Name", filter.SsPortfolio_Name);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));
        }

        protected override string GetHavingConditions(QueryRequest<ReportCompareFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            if (filter.sumQuantity?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSspStock.Quantity * stkxCurrentPrice.Price) >= @sumStockValueFrom");
                parameters.Add("@sumStockValueFrom", filter.sumQuantity.From.Value);
            }
            if (filter.sumQuantity?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSspStock.Quantity * stkxCurrentPrice.Price) <= @sumStockValueTo");
                parameters.Add("@sumStockValueTo", filter.sumQuantity.To.Value);
            }
            if (filter.sumPrice?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSsPortfolioBalance.Balance) >= @sumCashFrom");
                parameters.Add("@sumCashFrom", filter.sumPrice.From.Value);
            }
            if (filter.sumPrice?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSsPortfolioBalance.Balance) <= @sumCashTo");
                parameters.Add("@sumCashTo", filter.sumPrice.To.Value);
            }

            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));
        }
    }
}