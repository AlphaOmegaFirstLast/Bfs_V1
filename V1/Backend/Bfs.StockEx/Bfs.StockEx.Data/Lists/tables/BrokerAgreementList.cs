using Bfs.Core.Data;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data;
using System.Text;

namespace Bfs.StockEx.Data.Lists
{
    public class BrokerAgreementList: QueryBase<BrokerAgreementListFilter>,  IBrokerAgreementList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public BrokerAgreementList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BrokerAgreementListItem>> GetAsync(QueryRequest<BrokerAgreementListFilter> request)
        {
            var response = new QueryResponse<BrokerAgreementListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BrokerAgreementListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BrokerAgreementListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "BrokerAgreement", FieldName = "AgreementDate", DbName = "stkxBrokerAgreement.AgreementDate", QueryName = "AgreementDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BrokerAgreement", FieldName = "Id", DbName = "stkxBrokerAgreement.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BrokerAgreement", FieldName = "Notes", DbName = "stkxBrokerAgreement.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BrokerAgreement", FieldName = "OverdraftPrcnt", DbName = "stkxBrokerAgreement.OverdraftPrcnt", QueryName = "OverdraftPrcnt", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BrokerAgreement", FieldName = "OverdraftMx", DbName = "stkxBrokerAgreement.OverdraftMx", QueryName = "OverdraftMx", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BrokerAgreement", FieldName = "InvestorId", DbName = "stkxBrokerAgreement.InvestorId", QueryName = "InvestorId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BrokerAgreement", FieldName = "BrokerId", DbName = "stkxBrokerAgreement.BrokerId", QueryName = "BrokerId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BrokerAgreement", FieldName = "SsPortfolioId", DbName = "stkxBrokerAgreement.SsPortfolioId", QueryName = "SsPortfolioId", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "Investor", FieldName = "Name", DbName = "stkxInvestor.Name", QueryName = "InvestorName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Broker", FieldName = "Name", DbName = "stkxBroker.Name", QueryName = "BrokerName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Name", DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxBrokerAgreement ");

           sql.AppendLine($"   Left Join stkxInvestor on stkxBrokerAgreement.InvestorId = stkxInvestor.Id");
sql.AppendLine($"   Left Join stkxBroker on stkxBrokerAgreement.BrokerId = stkxBroker.Id");
sql.AppendLine($"   Left Join stkxSsPortfolio on stkxBrokerAgreement.SsPortfolioId = stkxSsPortfolio.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BrokerAgreementListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxBrokerAgreement.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxBrokerAgreement.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxBrokerAgreement.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.InvestorId.HasValue)
                {
                    sql.AppendLine("stkxBrokerAgreement.InvestorId = @InvestorId");
                    parameters.Add("@InvestorId", filter.InvestorId.Value);
                }
if (filter.BrokerId.HasValue)
                {
                    sql.AppendLine("stkxBrokerAgreement.BrokerId = @BrokerId");
                    parameters.Add("@BrokerId", filter.BrokerId.Value);
                }
if (filter.SsPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxBrokerAgreement.SsPortfolioId = @SsPortfolioId");
                    parameters.Add("@SsPortfolioId", filter.SsPortfolioId.Value);
                }

                if (filter.AgreementDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxBrokerAgreement.AgreementDate >= @AgreementDateFrom");
                    parameters.Add("@AgreementDateFrom", filter.AgreementDate.From.Value);
                }
                if (filter.AgreementDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxBrokerAgreement.AgreementDate <= @AgreementDateTo");
                    parameters.Add("@AgreementDateTo", filter.AgreementDate.To.Value);
                }
if (filter.OverdraftPrcnt?.From.HasValue == true)
                {
                    sql.AppendLine("stkxBrokerAgreement.OverdraftPrcnt >= @OverdraftPrcntFrom");
                    parameters.Add("@OverdraftPrcntFrom", filter.OverdraftPrcnt.From.Value);
                }
                if (filter.OverdraftPrcnt?.To.HasValue == true)
                {
                    sql.AppendLine("stkxBrokerAgreement.OverdraftPrcnt <= @OverdraftPrcntTo");
                    parameters.Add("@OverdraftPrcntTo", filter.OverdraftPrcnt.To.Value);
                }
if (filter.OverdraftMx?.From.HasValue == true)
                {
                    sql.AppendLine("stkxBrokerAgreement.OverdraftMx >= @OverdraftMxFrom");
                    parameters.Add("@OverdraftMxFrom", filter.OverdraftMx.From.Value);
                }
                if (filter.OverdraftMx?.To.HasValue == true)
                {
                    sql.AppendLine("stkxBrokerAgreement.OverdraftMx <= @OverdraftMxTo");
                    parameters.Add("@OverdraftMxTo", filter.OverdraftMx.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BrokerAgreementListFilter> request, DynamicParameters parameters)
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