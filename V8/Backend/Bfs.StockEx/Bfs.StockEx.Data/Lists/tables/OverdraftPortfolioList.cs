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
    public class OverdraftPortfolioList: QueryBase<OverdraftPortfolioListFilter>,  IOverdraftPortfolioList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public OverdraftPortfolioList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<OverdraftPortfolioListItem>> GetAsync(QueryRequest<OverdraftPortfolioListFilter> request)
        {
            var response = new QueryResponse<OverdraftPortfolioListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<OverdraftPortfolioListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<OverdraftPortfolioListItem> DoMapping(IEnumerable<OverdraftPortfolioListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (OverdraftPortfolioListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "Id", DbName = "stkxOverdraftPortfolio.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "Notes", DbName = "stkxOverdraftPortfolio.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "OverdraftPercent", DbName = "stkxOverdraftPortfolio.OverdraftPercent", QueryName = "OverdraftPercent", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "OverdraftMax", DbName = "stkxOverdraftPortfolio.OverdraftMax", QueryName = "OverdraftMax", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "SsPortfolioId", DbName = "stkxOverdraftPortfolio.SsPortfolioId", QueryName = "SsPortfolioId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "BrokerAgreementId", DbName = "stkxOverdraftPortfolio.BrokerAgreementId", QueryName = "BrokerAgreementId", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Name", DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false});

            //autoCompletes
            _fieldList.Add(new QueryField() {ComponentName = "BrokerAgreement", FieldName = "Name", DbName = "stkxBrokerAgreement.Name", QueryName = "BrokerAgreementName", IsAggregare = false});

           //Aggregates

//Template_Start_Code_DontOverwrite_5
             _fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "Name", DbName = "stkxOverdraftPortfolio.Name", QueryName = "AutoCompleteName", IsAggregare = false});
             _fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "Name", DbName = "stkxOverdraftPortfolio.Name", QueryName = "LookupName", IsAggregare = false});
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

        protected override string GetWhereConditions(QueryRequest<OverdraftPortfolioListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxOverdraftPortfolio.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxOverdraftPortfolio.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("stkxOverdraftPortfolio.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);                    
                   }
                   else
                   {
                      sql.AppendLine("stkxOverdraftPortfolio.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);
                   }
                }

                if (filter.SsPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.SsPortfolioId = @SsPortfolioId");
                    parameters.Add("@SsPortfolioId", filter.SsPortfolioId.Value);
                }

                if (filter.BrokerAgreementId.HasValue)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.BrokerAgreementId = @BrokerAgreementId");
                    parameters.Add("@BrokerAgreementId", filter.BrokerAgreementId.Value);
                }

                if (filter.OverdraftPercent?.From.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.OverdraftPercent >= @OverdraftPercentFrom");
                    parameters.Add("@OverdraftPercentFrom", filter.OverdraftPercent.From.Value);
                }
                if (filter.OverdraftPercent?.To.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.OverdraftPercent <= @OverdraftPercentTo");
                    parameters.Add("@OverdraftPercentTo", filter.OverdraftPercent.To.Value);
                }
if (filter.OverdraftMax?.From.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.OverdraftMax >= @OverdraftMaxFrom");
                    parameters.Add("@OverdraftMaxFrom", filter.OverdraftMax.From.Value);
                }
                if (filter.OverdraftMax?.To.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.OverdraftMax <= @OverdraftMaxTo");
                    parameters.Add("@OverdraftMaxTo", filter.OverdraftMax.To.Value);
                }

                //Template_Start_Code_DontOverwrite_7
                //Template_End_Code_DontOverwrite_7      
            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<OverdraftPortfolioListFilter> request, DynamicParameters parameters)
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