using Bfs.Core.Data;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data;
using Bfs.StockEx.Data.Interfaces;
using System.Text;

namespace Bfs.StockEx.Data.Reports
{
    public class NavReportCompare : QueryBase<NavReportCompareFilter>, INavReportCompare
    {
        private readonly IResourceSecurity _resourceSecurity;

        public NavReportCompare(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<NavReportCompareItem>> GetAsync(QueryRequest<NavReportCompareFilter> request)
        {
            var response = new QueryResponse<NavReportCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<NavReportCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<NavReportCompareItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "Sum(stkxCurrentPrice.price * stkxSspStock.Quantity)", QueryName = "StockValue", IsAggregare = true });
            _fieldList.Add(new QueryField() { DbName = "Sum(stkxSsPortfolioBalance.balance)", QueryName = "Cash", IsAggregare = true });
            _fieldList.Add(new QueryField() { DbName = "Sum(stkxSsPortfolioBalance.balance) + Sum(stkxCurrentPrice.price * stkxSspStock.Quantity)", QueryName = "NAV", IsAggregare = true });
        }

        protected override string GetFromJoinStatement()
        {
            var sql = new StringBuilder();
            sql.AppendLine(" From stkxSsPortfolio ");

            sql.AppendLine($"  Left Join stkxSspStock on stkxSsPortfolio.Id = stkxSspStock.SsPortfolioId");
            sql.AppendLine($"   Left Join stkxStockShare on stkxSspStock.StockShareId = stkxStockShare.Id");
            sql.AppendLine($"   left join stkxCurrentPrice on stkxSspStock.StockShareId = stkxCurrentPrice.StockShareId");
            sql.AppendLine($"   left join stkxSsPortfolioBalance on stkxSsPortfolio.Id = stkxSsPortfolioBalance.SsPortfolioId");
            sql.AppendLine($"   left join stkxCurrency  on stkxCurrency.Id = stkxStockShare.CurrencyId");

            return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<NavReportCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" stkxSsPortfolio.isDeleted=0 ");
            sql.AppendLine(" (stkxCurrentPrice.TransactionDate = CAST(GETDATE() AS DATE) or  stkxCurrentPrice.TransactionDate is null)");

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

        protected override string GetHavingConditions(QueryRequest<NavReportCompareFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            if (filter.StockValue?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxCurrentPrice.price * stkxSspStock.Quantity) >= @stockValueFrom");
                parameters.Add("@stockValueFrom", filter.StockValue.From.Value);
            }
            if (filter.StockValue?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxCurrentPrice.Price * stkxSspStock.Quantity) <= @stockValueFrom");
                parameters.Add("@stockValueTo", filter.StockValue.To.Value);
            }
            if (filter.Cash?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSsPortfolioBalance.Balance) >= @cashFrom");
                parameters.Add("@cashFrom", filter.Cash.From.Value);
            }
            if (filter.Cash?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSsPortfolioBalance.Balance) <= @cashTo");
                parameters.Add("@cashTo", filter.Cash.To.Value);
            }

            return string.Join(" And ", sql.ToString()
                                     .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(s => s.Trim()));

        }
    }
}

