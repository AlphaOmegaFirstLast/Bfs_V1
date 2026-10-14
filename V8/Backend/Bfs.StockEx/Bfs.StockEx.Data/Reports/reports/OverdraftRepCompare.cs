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
    public class OverdraftRepCompare :QueryBase<OverdraftRepCompareFilter>,  IOverdraftRepCompare
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
            //base fields
            _fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.Id", QueryName = "OverdraftPortfolio_Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.Notes", QueryName = "OverdraftPortfolio_Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.OverdraftPercent", QueryName = "OverdraftPortfolio_OverdraftPercent", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.OverdraftMax", QueryName = "OverdraftPortfolio_OverdraftMax", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.SsPortfolioId", QueryName = "OverdraftPortfolio_SsPortfolioId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxOverdraftPortfolio.BrokerAgreementId", QueryName = "OverdraftPortfolio_BrokerAgreementId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "stkxBrokerAgreement.Name", QueryName = "BrokerAgreementName", IsAggregare = false });

            //autoComplete

           //Aggregates

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxOverdraftPortfolio ");

           sql.AppendLine($"   Left Join stkxSsPortfolio on stkxOverdraftPortfolio.SsPortfolioId = stkxSsPortfolio.Id");
sql.AppendLine($"   Left Join stkxBrokerAgreement on stkxOverdraftPortfolio.BrokerAgreementId = stkxBrokerAgreement.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<OverdraftRepCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxOverdraftPortfolio.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.OverdraftPortfolio_Id.HasValue) && (filter.OverdraftPortfolio_Id>0))
                {
                    sql.AppendLine(" AND stkxOverdraftPortfolio.Id = @OverdraftPortfolio_Id");
                    parameters.Add("@OverdraftPortfolio_Id", filter.OverdraftPortfolio_Id);
                }

                if (!string.IsNullOrEmpty(filter.OverdraftPortfolio_Name))
                {
                    sql.AppendLine("stkxOverdraftPortfolio.Name like '%'+@OverdraftPortfolio_Name+'%' ");
                    parameters.Add("@OverdraftPortfolio_Name", filter.OverdraftPortfolio_Name);
                }

                if (filter.OverdraftPortfolio_SsPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.SsPortfolioId = @OverdraftPortfolio_SsPortfolioId");
                    parameters.Add("@OverdraftPortfolio_SsPortfolioId", filter.OverdraftPortfolio_SsPortfolioId.Value);
                }
if (filter.OverdraftPortfolio_BrokerAgreementId.HasValue)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.BrokerAgreementId = @OverdraftPortfolio_BrokerAgreementId");
                    parameters.Add("@OverdraftPortfolio_BrokerAgreementId", filter.OverdraftPortfolio_BrokerAgreementId.Value);
                }

                if (filter.OverdraftPortfolio_OverdraftPercent?.From.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.OverdraftPercent >= @OverdraftPortfolio_OverdraftPercentFrom");
                    parameters.Add("@OverdraftPortfolio_OverdraftPercentFrom", filter.OverdraftPortfolio_OverdraftPercent.From.Value);
                }
                if (filter.OverdraftPortfolio_OverdraftPercent?.To.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.OverdraftPercent <= @OverdraftPortfolio_OverdraftPercentTo");
                    parameters.Add("@OverdraftPortfolio_OverdraftPercentTo", filter.OverdraftPortfolio_OverdraftPercent.To.Value);
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