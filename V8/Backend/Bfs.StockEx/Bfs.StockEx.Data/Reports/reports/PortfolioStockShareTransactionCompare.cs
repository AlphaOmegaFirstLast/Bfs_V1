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
    public class PortfolioStockShareTransactionCompare :QueryBase<PortfolioStockShareTransactionCompareFilter>,  IPortfolioStockShareTransactionCompare
    {
        private readonly IResourceSecurity _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public PortfolioStockShareTransactionCompare(string connectionString, IResourceSecurity resourceSecurity
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

        public async Task<QueryResponse<PortfolioStockShareTransactionCompareItem>> GetAsync(QueryRequest<PortfolioStockShareTransactionCompareFilter> request)
        {
            var response = new QueryResponse<PortfolioStockShareTransactionCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<PortfolioStockShareTransactionCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<PortfolioStockShareTransactionCompareItem>)items;

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
_fieldList.Add(new QueryField() { DbName = "stkxSspTransaction.Quantity", QueryName = "SspTransaction_Quantity", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxSspTransaction.Price", QueryName = "SspTransaction_Price", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxSspTransaction.TransactionDate", QueryName = "SspTransaction_TransactionDate", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxStockShare.Name", QueryName = "StockShare_Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxTransactionType.Name", QueryName = "TransactionType_Name", IsAggregare = false });

            //lookups

            //autoComplete

           //Aggregates

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

        protected override string GetWhereConditions(QueryRequest<PortfolioStockShareTransactionCompareFilter> request, DynamicParameters parameters)
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
if (!string.IsNullOrEmpty(filter.StockShare_Name))
                {
                    sql.AppendLine("stkxStockShare.Name like '%'+@StockShare_Name+'%' ");
                    parameters.Add("@StockShare_Name", filter.StockShare_Name);
                }

                if (filter.SspTransaction_Quantity?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Quantity >= @SspTransaction_QuantityFrom");
                    parameters.Add("@SspTransaction_QuantityFrom", filter.SspTransaction_Quantity.From.Value);
                }
                if (filter.SspTransaction_Quantity?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Quantity <= @SspTransaction_QuantityTo");
                    parameters.Add("@SspTransaction_QuantityTo", filter.SspTransaction_Quantity.To.Value);
                }
if (filter.SspTransaction_Price?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Price >= @SspTransaction_PriceFrom");
                    parameters.Add("@SspTransaction_PriceFrom", filter.SspTransaction_Price.From.Value);
                }
                if (filter.SspTransaction_Price?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Price <= @SspTransaction_PriceTo");
                    parameters.Add("@SspTransaction_PriceTo", filter.SspTransaction_Price.To.Value);
                }
if (filter.SspTransaction_TransactionDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.TransactionDate >= @SspTransaction_TransactionDateFrom");
                    parameters.Add("@SspTransaction_TransactionDateFrom", filter.SspTransaction_TransactionDate.From.Value);
                }
                if (filter.SspTransaction_TransactionDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.TransactionDate <= @SspTransaction_TransactionDateTo");
                    parameters.Add("@SspTransaction_TransactionDateTo", filter.SspTransaction_TransactionDate.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<PortfolioStockShareTransactionCompareFilter> request, DynamicParameters parameters)
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
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5     
    }
}