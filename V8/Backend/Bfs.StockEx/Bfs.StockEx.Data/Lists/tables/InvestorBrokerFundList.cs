using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Lists
{
    public class InvestorBrokerFundList: QueryBase<InvestorBrokerFundListFilter>,  IInvestorBrokerFundList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public InvestorBrokerFundList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<InvestorBrokerFundListItem>> GetAsync(QueryRequest<InvestorBrokerFundListFilter> request)
        {
            var response = new QueryResponse<InvestorBrokerFundListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<InvestorBrokerFundListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<InvestorBrokerFundListItem> DoMapping(IEnumerable<InvestorBrokerFundListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (InvestorBrokerFundListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "InvestorBrokerFund", FieldName = "Id", DbName = "stkxInvestorBrokerFund.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "InvestorBrokerFund", FieldName = "Name", DbName = "stkxInvestorBrokerFund.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "InvestorBrokerFund", FieldName = "BrokerId", DbName = "stkxInvestorBrokerFund.BrokerId", QueryName = "BrokerId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "InvestorBrokerFund", FieldName = "InvestorId", DbName = "stkxInvestorBrokerFund.InvestorId", QueryName = "InvestorId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "InvestorBrokerFund", FieldName = "Fund", DbName = "stkxInvestorBrokerFund.Fund", QueryName = "Fund", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "InvestorBrokerFund", FieldName = "FundDate", DbName = "stkxInvestorBrokerFund.FundDate", QueryName = "FundDate", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "Broker", FieldName = "Name", DbName = "stkxBroker.Name", QueryName = "BrokerName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Investor", FieldName = "Name", DbName = "stkxInvestor.Name", QueryName = "InvestorName", IsAggregare = false});

            //autoCompletes

           //Aggregates

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxInvestorBrokerFund ");

           sql.AppendLine($"   Left Join stkxBroker on stkxInvestorBrokerFund.BrokerId = stkxBroker.Id");
sql.AppendLine($"   Left Join stkxInvestor on stkxInvestorBrokerFund.InvestorId = stkxInvestor.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<InvestorBrokerFundListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxInvestorBrokerFund.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxInvestorBrokerFund.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("stkxInvestorBrokerFund.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);                    
                   }
                   else
                   {
                      sql.AppendLine("stkxInvestorBrokerFund.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);
                   }
                }

                if (filter.BrokerId.HasValue)
                {
                    sql.AppendLine("stkxInvestorBrokerFund.BrokerId = @BrokerId");
                    parameters.Add("@BrokerId", filter.BrokerId.Value);
                }
if (filter.InvestorId.HasValue)
                {
                    sql.AppendLine("stkxInvestorBrokerFund.InvestorId = @InvestorId");
                    parameters.Add("@InvestorId", filter.InvestorId.Value);
                }

                if (filter.Fund?.From.HasValue == true)
                {
                    sql.AppendLine("stkxInvestorBrokerFund.Fund >= @FundFrom");
                    parameters.Add("@FundFrom", filter.Fund.From.Value);
                }
                if (filter.Fund?.To.HasValue == true)
                {
                    sql.AppendLine("stkxInvestorBrokerFund.Fund <= @FundTo");
                    parameters.Add("@FundTo", filter.Fund.To.Value);
                }
if (filter.FundDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxInvestorBrokerFund.FundDate >= @FundDateFrom");
                    parameters.Add("@FundDateFrom", filter.FundDate.From.Value);
                }
                if (filter.FundDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxInvestorBrokerFund.FundDate <= @FundDateTo");
                    parameters.Add("@FundDateTo", filter.FundDate.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<InvestorBrokerFundListFilter> request, DynamicParameters parameters)
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
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1       
    }
}