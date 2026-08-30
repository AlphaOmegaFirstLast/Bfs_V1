using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data;
using System.Text;

namespace Bfs.StockEx.Data.Lists
{
    public class SsPortfolioList: QueryBase<SsPortfolioListFilter>,  ISsPortfolioList
    {
        private readonly IResourceSecurity? _resourceSecurity;

        public SsPortfolioList(string connectionString, IResourceSecurity? resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<SsPortfolioListItem>> GetAsync(QueryRequest<SsPortfolioListFilter> request)
        {
            var response = new QueryResponse<SsPortfolioListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<SsPortfolioListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<SsPortfolioListItem> DoMapping(IEnumerable<SsPortfolioListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (SsPortfolioListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Id", DbName = "stkxSsPortfolio.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Name", DbName = "stkxSsPortfolio.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Notes", DbName = "stkxSsPortfolio.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "BrokerId", DbName = "stkxSsPortfolio.BrokerId", QueryName = "BrokerId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "InvestorId", DbName = "stkxSsPortfolio.InvestorId", QueryName = "InvestorId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Interest", DbName = "stkxSsPortfolio.Interest", QueryName = "Interest", IsAggregare = false});

            //object fields

            //lookups

            //autoCompletes
            _fieldList.Add(new QueryField() {ComponentName = "Broker", FieldName = "Name", DbName = "stkxBroker.Name", QueryName = "BrokerName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Investor", FieldName = "Name", DbName = "stkxInvestor.Name", QueryName = "InvestorName", IsAggregare = false});

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxSsPortfolio ");

           sql.AppendLine($"   Left Join stkxBroker on stkxSsPortfolio.BrokerId = stkxBroker.Id");
sql.AppendLine($"   Left Join stkxInvestor on stkxSsPortfolio.InvestorId = stkxInvestor.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<SsPortfolioListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxSsPortfolio.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxSsPortfolio.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxSsPortfolio.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.BrokerId.HasValue)
                {
                    sql.AppendLine("stkxSsPortfolio.BrokerId = @BrokerId");
                    parameters.Add("@BrokerId", filter.BrokerId.Value);
                }
if (filter.InvestorId.HasValue)
                {
                    sql.AppendLine("stkxSsPortfolio.InvestorId = @InvestorId");
                    parameters.Add("@InvestorId", filter.InvestorId.Value);
                }

                if (filter.Interest?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSsPortfolio.Interest >= @InterestFrom");
                    parameters.Add("@InterestFrom", filter.Interest.From.Value);
                }
                if (filter.Interest?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSsPortfolio.Interest <= @InterestTo");
                    parameters.Add("@InterestTo", filter.Interest.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<SsPortfolioListFilter> request, DynamicParameters parameters)
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

