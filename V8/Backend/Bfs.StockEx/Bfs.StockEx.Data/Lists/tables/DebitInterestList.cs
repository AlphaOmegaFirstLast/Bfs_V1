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
    public class DebitInterestList: QueryBase<DebitInterestListFilter>,  IDebitInterestList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public DebitInterestList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<DebitInterestListItem>> GetAsync(QueryRequest<DebitInterestListFilter> request)
        {
            var response = new QueryResponse<DebitInterestListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<DebitInterestListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<DebitInterestListItem> DoMapping(IEnumerable<DebitInterestListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (DebitInterestListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "DebitInterest", FieldName = "Id", DbName = "stkxDebitInterest.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DebitInterest", FieldName = "InterestPercent", DbName = "stkxDebitInterest.InterestPercent", QueryName = "InterestPercent", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DebitInterest", FieldName = "PaymentModeId", DbName = "stkxDebitInterest.PaymentModeId", QueryName = "PaymentModeId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DebitInterest", FieldName = "BrokerAgreementId", DbName = "stkxDebitInterest.BrokerAgreementId", QueryName = "BrokerAgreementId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DebitInterest", FieldName = "Notes", DbName = "stkxDebitInterest.Notes", QueryName = "Notes", IsAggregare = false});

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
           sql.AppendLine(" From stkxDebitInterest ");

           sql.AppendLine($"   Left Join stkxPaymentMode on stkxDebitInterest.PaymentModeId = stkxPaymentMode.Id");
sql.AppendLine($"   Left Join stkxBrokerAgreement on stkxDebitInterest.BrokerAgreementId = stkxBrokerAgreement.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<DebitInterestListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxDebitInterest.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxDebitInterest.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("stkxDebitInterest.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);                    
                   }
                   else
                   {
                      sql.AppendLine("stkxDebitInterest.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);
                   }
                }
if (!string.IsNullOrEmpty(filter.Notes))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("stkxDebitInterest.Notes like '%'+@Notes+'%' ");
                      parameters.Add("@Notes", filter.Notes);                    
                   }
                   else
                   {
                      sql.AppendLine("stkxDebitInterest.Notes like '%'+@Notes+'%' ");
                      parameters.Add("@Notes", filter.Notes);
                   }
                }

                if (filter.PaymentModeId.HasValue)
                {
                    sql.AppendLine("stkxDebitInterest.PaymentModeId = @PaymentModeId");
                    parameters.Add("@PaymentModeId", filter.PaymentModeId.Value);
                }
if (filter.BrokerAgreementId.HasValue)
                {
                    sql.AppendLine("stkxDebitInterest.BrokerAgreementId = @BrokerAgreementId");
                    parameters.Add("@BrokerAgreementId", filter.BrokerAgreementId.Value);
                }

                if (filter.InterestPercent?.From.HasValue == true)
                {
                    sql.AppendLine("stkxDebitInterest.InterestPercent >= @InterestPercentFrom");
                    parameters.Add("@InterestPercentFrom", filter.InterestPercent.From.Value);
                }
                if (filter.InterestPercent?.To.HasValue == true)
                {
                    sql.AppendLine("stkxDebitInterest.InterestPercent <= @InterestPercentTo");
                    parameters.Add("@InterestPercentTo", filter.InterestPercent.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<DebitInterestListFilter> request, DynamicParameters parameters)
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