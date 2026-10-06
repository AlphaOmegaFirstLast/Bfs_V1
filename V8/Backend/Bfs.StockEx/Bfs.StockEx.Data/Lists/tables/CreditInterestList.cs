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
    public class CreditInterestList: QueryBase<CreditInterestListFilter>,  ICreditInterestList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public CreditInterestList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<CreditInterestListItem>> GetAsync(QueryRequest<CreditInterestListFilter> request)
        {
            var response = new QueryResponse<CreditInterestListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<CreditInterestListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<CreditInterestListItem> DoMapping(IEnumerable<CreditInterestListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (CreditInterestListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "CreditInterest", FieldName = "Id", DbName = "stkxCreditInterest.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CreditInterest", FieldName = "InterestPercent", DbName = "stkxCreditInterest.InterestPercent", QueryName = "InterestPercent", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CreditInterest", FieldName = "PaymentModeId", DbName = "stkxCreditInterest.PaymentModeId", QueryName = "PaymentModeId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CreditInterest", FieldName = "BrokerAgreementId", DbName = "stkxCreditInterest.BrokerAgreementId", QueryName = "BrokerAgreementId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CreditInterest", FieldName = "Notes", DbName = "stkxCreditInterest.Notes", QueryName = "Notes", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "PaymentMode", FieldName = "Name", DbName = "stkxPaymentMode.Name", QueryName = "PaymentModeName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BrokerAgreement", FieldName = "Name", DbName = "stkxBrokerAgreement.Name", QueryName = "BrokerAgreementName", IsAggregare = false});

            //autoCompletes

           //Aggregates

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxCreditInterest ");

           sql.AppendLine($"   Left Join stkxPaymentMode on stkxCreditInterest.PaymentModeId = stkxPaymentMode.Id");
sql.AppendLine($"   Left Join stkxBrokerAgreement on stkxCreditInterest.BrokerAgreementId = stkxBrokerAgreement.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<CreditInterestListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxCreditInterest.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxCreditInterest.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("stkxCreditInterest.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);                    
                   }
                   else
                   {
                      sql.AppendLine("stkxCreditInterest.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);
                   }
                }
if (!string.IsNullOrEmpty(filter.Notes))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("stkxCreditInterest.Notes like '%'+@Notes+'%' ");
                      parameters.Add("@Notes", filter.Notes);                    
                   }
                   else
                   {
                      sql.AppendLine("stkxCreditInterest.Notes like '%'+@Notes+'%' ");
                      parameters.Add("@Notes", filter.Notes);
                   }
                }

                if (filter.PaymentModeId.HasValue)
                {
                    sql.AppendLine("stkxCreditInterest.PaymentModeId = @PaymentModeId");
                    parameters.Add("@PaymentModeId", filter.PaymentModeId.Value);
                }
if (filter.BrokerAgreementId.HasValue)
                {
                    sql.AppendLine("stkxCreditInterest.BrokerAgreementId = @BrokerAgreementId");
                    parameters.Add("@BrokerAgreementId", filter.BrokerAgreementId.Value);
                }

                if (filter.InterestPercent?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCreditInterest.InterestPercent >= @InterestPercentFrom");
                    parameters.Add("@InterestPercentFrom", filter.InterestPercent.From.Value);
                }
                if (filter.InterestPercent?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCreditInterest.InterestPercent <= @InterestPercentTo");
                    parameters.Add("@InterestPercentTo", filter.InterestPercent.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<CreditInterestListFilter> request, DynamicParameters parameters)
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