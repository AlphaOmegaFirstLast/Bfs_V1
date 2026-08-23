using Bfs.Core.Data;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data;
using Bfs.StockEx.Data.Interfaces;
using System.Text;

namespace Bfs.StockEx.Data.Reports
{
    public class PortfolioCashTransactionCompare :QueryBase<PortfolioCashTransactionCompareFilter>,  IPortfolioCashTransactionCompare
    {
        private readonly IResourceSecurity _resourceSecurity;

        public PortfolioCashTransactionCompare(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<PortfolioCashTransactionCompareItem>> GetAsync(QueryRequest<PortfolioCashTransactionCompareFilter> request)
        {
            var response = new QueryResponse<PortfolioCashTransactionCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<PortfolioCashTransactionCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<PortfolioCashTransactionCompareItem>)items;

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
_fieldList.Add(new QueryField() { DbName = "stkxCashTransaction.Value", QueryName = "CashTransaction_Value", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxCashTransaction.TransactionDate", QueryName = "CashTransaction_TransactionDate", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxTransactionType.Name", QueryName = "TransactionType_Name", IsAggregare = false });

            //lookups

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxSsPortfolio ");

           sql.AppendLine($"   Left Join stkxBroker on stkxSsPortfolio.BrokerId = stkxBroker.Id");
sql.AppendLine($"   Left Join stkxInvestor on stkxSsPortfolio.InvestorId = stkxInvestor.Id");
sql.AppendLine($"   Left Join stkxCashTransaction on stkxCashTransaction.SsPortfolioId = stkxSsPortfolio.Id");
sql.AppendLine($"   Left Join stkxTransactionType on stkxCashTransaction.TransactionTypeId = stkxTransactionType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<PortfolioCashTransactionCompareFilter> request, DynamicParameters parameters)
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

                if (filter.CashTransaction_Value?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCashTransaction.Value >= @CashTransaction_ValueFrom");
                    parameters.Add("@CashTransaction_ValueFrom", filter.CashTransaction_Value.From.Value);
                }
                if (filter.CashTransaction_Value?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCashTransaction.Value <= @CashTransaction_ValueTo");
                    parameters.Add("@CashTransaction_ValueTo", filter.CashTransaction_Value.To.Value);
                }
if (filter.CashTransaction_TransactionDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCashTransaction.TransactionDate >= @CashTransaction_TransactionDateFrom");
                    parameters.Add("@CashTransaction_TransactionDateFrom", filter.CashTransaction_TransactionDate.From.Value);
                }
                if (filter.CashTransaction_TransactionDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCashTransaction.TransactionDate <= @CashTransaction_TransactionDateTo");
                    parameters.Add("@CashTransaction_TransactionDateTo", filter.CashTransaction_TransactionDate.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<PortfolioCashTransactionCompareFilter> request, DynamicParameters parameters)
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