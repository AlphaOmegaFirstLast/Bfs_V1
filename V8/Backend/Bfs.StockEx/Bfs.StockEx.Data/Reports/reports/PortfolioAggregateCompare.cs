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
    public class PortfolioAggregateCompare :QueryBase<PortfolioAggregateCompareFilter>,  IPortfolioAggregateCompare
    {
        private readonly IResourceSecurity _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public PortfolioAggregateCompare(string connectionString, IResourceSecurity resourceSecurity
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

        public async Task<QueryResponse<PortfolioAggregateCompareItem>> GetAsync(QueryRequest<PortfolioAggregateCompareFilter> request)
        {
            var response = new QueryResponse<PortfolioAggregateCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<PortfolioAggregateCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<PortfolioAggregateCompareItem>)items;

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
_fieldList.Add(new QueryField() { DbName = "stkxBroker.Name", QueryName = "Broker_Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxInvestor.Name", QueryName = "Investor_Name", IsAggregare = false });

            //lookups

            //autoComplete

           //Aggregates
           _fieldList.Add(new QueryField() { DbName = "Sum(stkxSspTransaction.quantity)", QueryName = "sumQuantity", IsAggregare = true });
_fieldList.Add(new QueryField() { DbName = "Sum(stkxSspTransaction.price)", QueryName = "sumPrice", IsAggregare = true });

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxSsPortfolio ");

           sql.AppendLine($"   Left Join stkxBroker on stkxSsPortfolio.BrokerId = stkxBroker.Id");
sql.AppendLine($"   Left Join stkxInvestor on stkxSsPortfolio.InvestorId = stkxInvestor.Id");
sql.AppendLine($"   Left Join stkxSspTransaction on stkxSspTransaction.SsPortfolioId = stkxSsPortfolio.Id");
sql.AppendLine($"   Left Join stkxStockShare on stkxSspTransaction.StockShareId = stkxStockShare.Id");
sql.AppendLine($"   Left Join stkxTransactionType on stkxSspTransaction.TransactionTypeId = stkxTransactionType.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<PortfolioAggregateCompareFilter> request, DynamicParameters parameters)
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

        protected override string GetHavingConditions(QueryRequest<PortfolioAggregateCompareFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            if (filter.sumQuantity?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSspTransaction.Quantity) >= @sumQuantityFrom");
                parameters.Add("@sumQuantityFrom", filter.sumQuantity.From.Value);
            }
            if (filter.sumQuantity?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSspTransaction.Quantity) <= @sumQuantityTo");
                parameters.Add("@sumQuantityTo", filter.sumQuantity.To.Value);
            }
if (filter.sumPrice?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSspTransaction.Price) >= @sumPriceFrom");
                parameters.Add("@sumPriceFrom", filter.sumPrice.From.Value);
            }
            if (filter.sumPrice?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSspTransaction.Price) <= @sumPriceTo");
                parameters.Add("@sumPriceTo", filter.sumPrice.To.Value);
            }

            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
       } 
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5     
    }
}