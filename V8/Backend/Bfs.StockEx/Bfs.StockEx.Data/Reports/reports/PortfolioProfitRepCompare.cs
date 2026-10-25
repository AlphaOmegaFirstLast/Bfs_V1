using Bfs.Core.Data;
using Bfs.Core.Services.Security;
using Bfs.StockEx.Data;
using Bfs.StockEx.Data.Interfaces;
using Dapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Data.SqlClient;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Reports
{
    public class PortfolioProfitRepCompare :QueryBase<PortfolioProfitRepCompareFilter>,  IPortfolioProfitRepCompare
    {
        private readonly IResourceSecurity _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public PortfolioProfitRepCompare(string connectionString, IResourceSecurity resourceSecurity
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

        public async Task<QueryResponse<PortfolioProfitRepCompareItem>> GetAsync(QueryRequest<PortfolioProfitRepCompareFilter> request)
        {
            var response = new QueryResponse<PortfolioProfitRepCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<PortfolioProfitRepCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<PortfolioProfitRepCompareItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "stkxSspStock.SsPortfolioId", QueryName = "SspStock_SsPortfolioId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxSspStock.Quantity", QueryName = "SspStock_Quantity", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxSspStock.StockShareId", QueryName = "SspStock_StockShareId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxSspStock.AverageCost", QueryName = "SspStock_AverageCost", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxSspStock.CurrencyId", QueryName = "SspStock_CurrencyId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxCurrentPrice.Price", QueryName = "CurrentPrice_Price", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "(stkxSspStock.Quantity * (stkxCurrentPrice.Price - stkxSspStock.AverageCost))", QueryName = "CurrentPrice_Profit", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxStockShare.Name", QueryName = "StockShareName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxCurrency.Name", QueryName = "CurrencyName", IsAggregare = false });

            //autoComplete

            //Aggregates

            //Template_Start_Code_DontOverwrite_5
            
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxSspStock ");

           sql.AppendLine($"   Left Join stkxSsPortfolio on stkxSspStock.SsPortfolioId = stkxSsPortfolio.Id");
sql.AppendLine($"   Left Join stkxStockShare on stkxSspStock.StockShareId = stkxStockShare.Id");
sql.AppendLine($"   Left Join stkxCurrency on stkxSspStock.CurrencyId = stkxCurrency.Id");

            //Template_Start_Code_DontOverwrite_6
            sql.AppendLine($"   Left Join stkxCurrentPrice on stkxCurrentPrice.StockShareId = stkxStockShare.Id");
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<PortfolioProfitRepCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxSspStock.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (filter.SspStock_SsPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxSspStock.SsPortfolioId = @SspStock_SsPortfolioId");
                    parameters.Add("@SspStock_SsPortfolioId", filter.SspStock_SsPortfolioId.Value);
                }
if (filter.SspStock_StockShareId.HasValue)
                {
                    sql.AppendLine("stkxSspStock.StockShareId = @SspStock_StockShareId");
                    parameters.Add("@SspStock_StockShareId", filter.SspStock_StockShareId.Value);
                }
if (filter.SspStock_CurrencyId.HasValue)
                {
                    sql.AppendLine("stkxSspStock.CurrencyId = @SspStock_CurrencyId");
                    parameters.Add("@SspStock_CurrencyId", filter.SspStock_CurrencyId.Value);
                }

                if (filter.CurrentPrice_Profit?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCurrentPrice.Profit >= @CurrentPrice_ProfitFrom");
                    parameters.Add("@CurrentPrice_ProfitFrom", filter.CurrentPrice_Profit.From.Value);
                }
                if (filter.CurrentPrice_Profit?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCurrentPrice.Profit <= @CurrentPrice_ProfitTo");
                    parameters.Add("@CurrentPrice_ProfitTo", filter.CurrentPrice_Profit.To.Value);
                }

                //Template_Start_Code_DontOverwrite_7
                //Template_End_Code_DontOverwrite_7      
            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<PortfolioProfitRepCompareFilter> request, DynamicParameters parameters)
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