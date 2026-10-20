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
_fieldList.Add(new QueryField() { DbName = "stkxSsPortfolio.InvestorId", QueryName = "SsPortfolio_InvestorId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxSsPortfolio.BrokerId", QueryName = "SsPortfolio_BrokerId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "stkxInvestor.Name", QueryName = "InvestorName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxBroker.Name", QueryName = "BrokerName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false });

            //autoComplete

           //Aggregates
           //Template_Start_Generated_Ignored_1
/*

_fieldList.Add(new QueryField() { DbName = "Sum(stkxCurrentPrice.value)", QueryName = "sumValue", IsAggregare = true });
_fieldList.Add(new QueryField() { DbName = "Sum(stkxSsPortfolioBalance.balance)", QueryName = "sumBalance", IsAggregare = true });
_fieldList.Add(new QueryField() { DbName = "Sum(stkxSsPortfolioBalance.nav)", QueryName = "sumNav", IsAggregare = true });
*/
//Template_End_Generated_Ignored_1

//Template_Start_Code_DontOverwrite_5
_fieldList.Add(new QueryField() { DbName = "Sum(isnull(CP.price,0) * isnull(stkxSspStock.Quantity,0))", QueryName = "sumValue", IsAggregare = true });
            _fieldList.Add(new QueryField() { DbName = "Sum(isnull(stkxSsPortfolioBalance.balance,0))", QueryName = "sumBalance", IsAggregare = true });
            _fieldList.Add(new QueryField() { DbName = "Sum(isnull(stkxSsPortfolioBalance.balance,0)) + Sum(isnull(CP.price,0) * isnull(stkxSspStock.Quantity,0))", QueryName = "sumNav", IsAggregare = true });
//Template_End_Code_DontOverwrite_5

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxSsPortfolio ");

           sql.AppendLine($"   Left Join stkxInvestor on stkxSsPortfolio.InvestorId = stkxInvestor.Id");
sql.AppendLine($"   Left Join stkxBroker on stkxSsPortfolio.BrokerId = stkxBroker.Id");
//Template_Start_Generated_Ignored_2
/*

sql.AppendLine($"   Left Join stkxSsPortfolio on stkxSsPortfolio.SsPortfolioId = stkxSsPortfolio.Id");
*/
//Template_End_Generated_Ignored_2

//Template_Start_Code_DontOverwrite_6
sql.AppendLine($"  left join stkxSsPortfolioBalance on stkxSsPortfolio.Id = stkxSsPortfolioBalance.SsPortfolioId");
            sql.AppendLine($"  Left Join stkxSspStock on stkxSsPortfolio.Id = stkxSspStock.SsPortfolioId");

            sql.AppendLine($"  Left Join stkxStockShare S ON stkxSspStock.StockShareId = s.Id");
            sql.AppendLine($"  Left Join stkxCurrency  on stkxCurrency.Id = s.CurrencyId");
            sql.AppendLine($"  OUTER APPLY ( SELECT TOP 1 Price  FROM [stkxCurrentPrice] cp_sub  WHERE cp_sub.StockShareId = S.Id ORDER BY cp_sub.TransactionDate DESC) CP");
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

                if (filter.SsPortfolio_InvestorId.HasValue)
                {
                    sql.AppendLine("stkxSsPortfolio.InvestorId = @SsPortfolio_InvestorId");
                    parameters.Add("@SsPortfolio_InvestorId", filter.SsPortfolio_InvestorId.Value);
                }
if (filter.SsPortfolio_BrokerId.HasValue)
                {
                    sql.AppendLine("stkxSsPortfolio.BrokerId = @SsPortfolio_BrokerId");
                    parameters.Add("@SsPortfolio_BrokerId", filter.SsPortfolio_BrokerId.Value);
                }
//Template_Start_Generated_Ignored_3
/*

if (filter.SsPortfolio_SsPortfolioId.HasValue)
                                {
                                    sql.AppendLine("stkxSsPortfolio.SsPortfolioId = @SsPortfolio_SsPortfolioId");
                                    parameters.Add("@SsPortfolio_SsPortfolioId", filter.SsPortfolio_SsPortfolioId.Value);
                                }
*/
//Template_End_Generated_Ignored_3

                //Template_Start_Code_DontOverwrite_7
//Template_End_Code_DontOverwrite_7

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

            if (filter.sumValue?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxCurrentPrice.Value) >= @sumValueFrom");
                parameters.Add("@sumValueFrom", filter.sumValue.From.Value);
            }
            if (filter.sumValue?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxCurrentPrice.Value) <= @sumValueTo");
                parameters.Add("@sumValueTo", filter.sumValue.To.Value);
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
//Template_Start_Generated_Ignored_4
/*

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
*/
//Template_End_Generated_Ignored_4

            //Template_Start_Code_DontOverwrite_8
if (filter.sumValue?.From.HasValue == true)
            {
                sql.AppendLine("Sum(isnull(CP.price,0) * isnull(stkxSspStock.Quantity,0)) >= @sumValueFrom");
                parameters.Add("@sumValueFrom", filter.sumValue.From.Value);
            }
            if (filter.sumValue?.To.HasValue == true)
            {
                sql.AppendLine("Sum(isnull(CP.price,0) * isnull(stkxSspStock.Quantity,0)) <= @sumValueTo");
                parameters.Add("@sumValueTo", filter.sumValue.To.Value);
            }
            if (filter.sumBalance?.From.HasValue == true)
            {
                sql.AppendLine("Sum(isnull(stkxSsPortfolioBalance.balance,0)) >= @sumBalanceFrom");
                parameters.Add("@sumBalanceFrom", filter.sumBalance.From.Value);
            }
            if (filter.sumBalance?.To.HasValue == true)
            {
                sql.AppendLine("Sum(isnull(stkxSsPortfolioBalance.balance,0)) <= @sumBalanceTo");
                parameters.Add("@sumBalanceTo", filter.sumBalance.To.Value);
            }
            if (filter.sumNav?.From.HasValue == true)
            {
                sql.AppendLine("Sum(isnull(stkxSsPortfolioBalance.balance,0)) + Sum(isnull(CP.price,0) * isnull(stkxSspStock.Quantity,0)) >= @sumNavFrom");
                parameters.Add("@sumNavFrom", filter.sumNav.From.Value);
            }
            if (filter.sumNav?.To.HasValue == true)
            {
                sql.AppendLine("Sum(isnull(stkxSsPortfolioBalance.balance,0)) + Sum(isnull(CP.price,0) * isnull(stkxSspStock.Quantity,0)) <= @sumNavTo");
                parameters.Add("@sumNavTo", filter.sumNav.To.Value);
            }
//Template_End_Code_DontOverwrite_8

            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
       } 
//Template_Start_Code_DontOverwrite_9
//Template_End_Code_DontOverwrite_9

    }
}