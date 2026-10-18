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
    public class OverdraftRepCompare : QueryBase<OverdraftRepCompareFilter>, IOverdraftRepCompare
    {
        private readonly IResourceSecurity _resourceSecurity;
        //Template_Start_Code_DontOverwrite_2
        //Template_End_Code_DontOverwrite_2

        public OverdraftRepCompare(string connectionString, IResourceSecurity resourceSecurity
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

        public async Task<QueryResponse<OverdraftRepCompareItem>> GetAsync(QueryRequest<OverdraftRepCompareFilter> request)
        {
            var response = new QueryResponse<OverdraftRepCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<OverdraftRepCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<OverdraftRepCompareItem>)items;

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        protected override void SetupFields()
        {
            //Template_Start_Generated_Ignored_1
            /*
            //base fields
            _fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.Id", QueryName = "OverdraftPortfolio_Id", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.OverdraftMax", QueryName = "OverdraftPortfolio_OverdraftMax", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxBrokerAgreement.SsPortfolioId", QueryName = "BrokerAgreement_SsPortfolioId", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.BrokerAgreementId", QueryName = "OverdraftPortfolio_BrokerAgreementId", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxBrokerAgreement.BrokerId", QueryName = "BrokerAgreement_BrokerId", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxBrokerAgreement.InvestorId", QueryName = "BrokerAgreement_InvestorId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxBrokerAgreement.Name", QueryName = "BrokerAgreementName", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxBroker.Name", QueryName = "BrokerName", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxInvestor.Name", QueryName = "InvestorName", IsAggregare = false });

            //autoComplete

            //Aggregates

            _fieldList.Add(new QueryField() { DbName = "Sum(stkxCashTransaction.totalCashDeposit)", QueryName = "sumTotalCashDeposit", IsAggregare = true });
            _fieldList.Add(new QueryField() { DbName = "Sum(stkxSsPortfolioBalance.availableCredit)", QueryName = "sumAvailableCredit", IsAggregare = true });
        */
            //Template_End_Generated_Ignored_1

            //Template_Start_Code_DontOverwrite_5
            _fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.Id", QueryName = "OverdraftPortfolio_Id", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.OverdraftMax", QueryName = "OverdraftPortfolio_OverdraftMax", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.OverdraftPercent", QueryName = "OverdraftPortfolio_OverdraftPercent", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.BrokerAgreementId", QueryName = "OverdraftPortfolio_BrokerAgreementId", IsAggregare = false });
         
            _fieldList.Add(new QueryField() { DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxBroker.Name", QueryName = "BrokerName", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxInvestor.Name", QueryName = "InvestorName", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxCurrency.Name", QueryName = "CurrencyName", IsAggregare = false });

            _fieldList.Add(new QueryField() { DbName = "stkxBrokerAgreement.AgreementDate", QueryName = "BrokerAgreement_AgreementDate", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxBrokerAgreement.BrokerId", QueryName = "BrokerAgreement_BrokerId", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxBrokerAgreement.InvestorId", QueryName = "BrokerAgreement_InvestorId", IsAggregare = false });
            _fieldList.Add(new QueryField() { DbName = "stkxSsPortfolioBalance.Balance", QueryName = "SsPortfolioBalance_Balance", IsAggregare = false });

            var sb1 = new StringBuilder();
            sb1.Append("  CASE WHEN ");
            sb1.Append("       SUM(stkxCashTransaction.value) * stkxOverdraftPortfolio.OverdraftPercent/100 > stkxOverdraftPortfolio.OverdraftMax ");
            sb1.Append("  THEN stkxOverdraftPortfolio.OverdraftMax ");
            sb1.Append("  ELSE SUM(stkxCashTransaction.value) * stkxOverdraftPortfolio.OverdraftPercent/100 ");
            sb1.Append("  END ");

            _fieldList.Add(new QueryField() { DbName = sb1.ToString(), QueryName = "OverdraftPortfolio_Overdraft", IsAggregare = true }); 
        
            var sb2 = new StringBuilder();
            sb2.Append("  ((CASE WHEN ");
            sb2.Append("       SUM(stkxCashTransaction.value) * stkxOverdraftPortfolio.OverdraftPercent/100 > stkxOverdraftPortfolio.OverdraftMax  ");
            sb2.Append("  THEN stkxOverdraftPortfolio.OverdraftMax ");
            sb2.Append("  ELSE SUM(stkxCashTransaction.value) * stkxOverdraftPortfolio.OverdraftPercent/100 ");
            sb2.Append("  END) ");
            sb2.Append("  + Balance) ");

            _fieldList.Add(new QueryField() { DbName = sb2.ToString(), QueryName = "sumAvailableCredit", IsAggregare = true }); 
            _fieldList.Add(new QueryField() { DbName = "Sum(stkxCashTransaction.value)", QueryName = "sumTotalCashDeposit", IsAggregare = true });

            //Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
            var sql = new StringBuilder();
            sql.AppendLine(" From stkxOverdraftPortfolio ");
            //Template_Start_Generated_Ignored_1
            /*
            sql.AppendLine($"   Left Join stkxSsPortfolio on stkxBrokerAgreement.SsPortfolioId = stkxSsPortfolio.Id");
            sql.AppendLine($"   Left Join stkxBrokerAgreement on stkxOverdraftPortfolio.BrokerAgreementId = stkxBrokerAgreement.Id");
            sql.AppendLine($"   Left Join stkxBroker on stkxBrokerAgreement.BrokerId = stkxBroker.Id");
            sql.AppendLine($"   Left Join stkxInvestor on stkxBrokerAgreement.InvestorId = stkxInvestor.Id");
            */
            //Template_End_Generated_Ignored_1

            //Template_Start_Code_DontOverwrite_6
            sql.AppendLine($"   Left Join  stkxSsPortfolio on stkxOverdraftPortfolio.SsPortfolioId = stkxSsPortfolio.Id");
            sql.AppendLine($"   Left Join  stkxSsPortfolioBalance on stkxSsPortfolio.id = stkxSsPortfolioBalance.SsPortfolioId ");
            sql.AppendLine($"   Left Join  stkxCurrency on stkxSsPortfolioBalance.CurrencyId = stkxCurrency.Id ");
            sql.AppendLine($"   Left Join  stkxCashTransaction on stkxSsPortfolio.id = stkxCashTransaction.SsPortfolioId  and stkxCashTransaction.TransactionTypeId = 3");

            sql.AppendLine($"   Left Join  stkxBrokerAgreement on stkxOverdraftPortfolio.BrokerAgreementId = stkxBrokerAgreement.id");
            sql.AppendLine($"   Left Join  stkxBroker on  stkxBrokerAgreement.BrokerId = stkxBroker.id");
            sql.AppendLine($"   Left Join  stkxInvestor on stkxBrokerAgreement.InvestorId = stkxInvestor.id ");
            //Template_End_Code_DontOverwrite_6

            return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<OverdraftRepCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" stkxOverdraftPortfolio.isDeleted=0 ");

            var filter = request.Filter;
            if (filter != null)
            {
                if ((filter.OverdraftPortfolio_Id.HasValue) && (filter.OverdraftPortfolio_Id > 0))
                {
                    sql.AppendLine(" AND stkxOverdraftPortfolio.Id = @OverdraftPortfolio_Id");
                    parameters.Add("@OverdraftPortfolio_Id", filter.OverdraftPortfolio_Id);
                }
                if (filter.OverdraftPortfolio_BrokerAgreementId.HasValue)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.BrokerAgreementId = @OverdraftPortfolio_BrokerAgreementId");
                    parameters.Add("@OverdraftPortfolio_BrokerAgreementId", filter.OverdraftPortfolio_BrokerAgreementId.Value);
                }
                if (filter.BrokerAgreement_BrokerId.HasValue)
                {
                    sql.AppendLine("stkxBrokerAgreement.BrokerId = @BrokerAgreement_BrokerId");
                    parameters.Add("@BrokerAgreement_BrokerId", filter.BrokerAgreement_BrokerId.Value);
                }
                if (filter.BrokerAgreement_InvestorId.HasValue)
                {
                    sql.AppendLine("stkxBrokerAgreement.InvestorId = @BrokerAgreement_InvestorId");
                    parameters.Add("@BrokerAgreement_InvestorId", filter.BrokerAgreement_InvestorId.Value);
                }

                if (filter.OverdraftPortfolio_OverdraftMax?.From.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.OverdraftMax >= @OverdraftPortfolio_OverdraftMaxFrom");
                    parameters.Add("@OverdraftPortfolio_OverdraftMaxFrom", filter.OverdraftPortfolio_OverdraftMax.From.Value);
                }
                if (filter.OverdraftPortfolio_OverdraftMax?.To.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.OverdraftMax <= @OverdraftPortfolio_OverdraftMaxTo");
                    parameters.Add("@OverdraftPortfolio_OverdraftMaxTo", filter.OverdraftPortfolio_OverdraftMax.To.Value);
                }
                if (filter.OverdraftPortfolio_Overdraft?.From.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.Overdraft >= @OverdraftPortfolio_OverdraftFrom");
                    parameters.Add("@OverdraftPortfolio_OverdraftFrom", filter.OverdraftPortfolio_Overdraft.From.Value);
                }
                if (filter.OverdraftPortfolio_Overdraft?.To.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.Overdraft <= @OverdraftPortfolio_OverdraftTo");
                    parameters.Add("@OverdraftPortfolio_OverdraftTo", filter.OverdraftPortfolio_Overdraft.To.Value);
                }

                //Template_Start_Code_DontOverwrite_7
                //Template_End_Code_DontOverwrite_7      
            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));
        }

        protected override string GetHavingConditions(QueryRequest<OverdraftRepCompareFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            if (filter.sumTotalCashDeposit?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxCashTransaction.TotalCashDeposit) >= @sumTotalCashDepositFrom");
                parameters.Add("@sumTotalCashDepositFrom", filter.sumTotalCashDeposit.From.Value);
            }
            if (filter.sumTotalCashDeposit?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxCashTransaction.TotalCashDeposit) <= @sumTotalCashDepositTo");
                parameters.Add("@sumTotalCashDepositTo", filter.sumTotalCashDeposit.To.Value);
            }
            if (filter.sumAvailableCredit?.From.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSsPortfolioBalance.AvailableCredit) >= @sumAvailableCreditFrom");
                parameters.Add("@sumAvailableCreditFrom", filter.sumAvailableCredit.From.Value);
            }
            if (filter.sumAvailableCredit?.To.HasValue == true)
            {
                sql.AppendLine("Sum(stkxSsPortfolioBalance.AvailableCredit) <= @sumAvailableCreditTo");
                parameters.Add("@sumAvailableCreditTo", filter.sumAvailableCredit.To.Value);
            }

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