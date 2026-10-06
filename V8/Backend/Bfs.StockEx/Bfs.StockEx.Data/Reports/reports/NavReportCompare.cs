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
    public class NavReportCompare :QueryBase<NavReportCompareFilter>,  INavReportCompare
    {
        private readonly IResourceSecurity _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public NavReportCompare(string connectionString, IResourceSecurity resourceSecurity
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
           _fieldList.Add(new QueryField() { DbName = "Sum(stkxCurrentPrice.price)", QueryName = "sumPrice", IsAggregare = true });
_fieldList.Add(new QueryField() { DbName = "Sum(stkxSsPortfolioBalance.balance)", QueryName = "sumBalance", IsAggregare = true });
_fieldList.Add(new QueryField() { DbName = "Sum(stkxSsPortfolioBalance.nav)", QueryName = "sumNav", IsAggregare = true });

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxSsPortfolio ");

           sql.AppendLine($"   Left Join stkxStockShare on stkxSspStock.StockShareId = stkxStockShare.Id");
sql.AppendLine($"   Left Join stkxCurrency on stkxStockShare.CurrencyId = stkxCurrency.Id");
sql.AppendLine($"   Left Join stkxSspStock on stkxSspStock.SsPortfolioId = stkxSsPortfolio.Id");
sql.AppendLine($"   Left Join stkxStockShareId on stkxCurrentPrice.StockShareIdId = stkxStockShareId.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<NavReportCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
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

        protected override string GetHavingConditions(QueryRequest<NavReportCompareFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            if (filter.sumPrice?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxCurrentPrice.Price) >= @sumPriceFrom");
                parameters.Add("@sumPriceFrom", filter.sumPrice.From.Value);
            }
            if (filter.sumPrice?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxCurrentPrice.Price) <= @sumPriceTo");
                parameters.Add("@sumPriceTo", filter.sumPrice.To.Value);
            }
if (filter.sumBalance?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSsPortfolioBalance.Balance) >= @sumBalanceFrom");
                parameters.Add("@sumBalanceFrom", filter.sumBalance.From.Value);
            }
            if (filter.sumBalance?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSsPortfolioBalance.Balance) <= @sumBalanceTo");
                parameters.Add("@sumBalanceTo", filter.sumBalance.To.Value);
            }
if (filter.sumNav?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSsPortfolioBalance.Nav) >= @sumNavFrom");
                parameters.Add("@sumNavFrom", filter.sumNav.From.Value);
            }
            if (filter.sumNav?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSsPortfolioBalance.Nav) <= @sumNavTo");
                parameters.Add("@sumNavTo", filter.sumNav.To.Value);
            }

            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
       } 
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5     
    }
}