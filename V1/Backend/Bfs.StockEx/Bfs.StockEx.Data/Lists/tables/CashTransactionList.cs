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
    public class CashTransactionList: QueryBase<CashTransactionListFilter>,  ICashTransactionList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public CashTransactionList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<CashTransactionListItem>> GetAsync(QueryRequest<CashTransactionListFilter> request)
        {
            var response = new QueryResponse<CashTransactionListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<CashTransactionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<CashTransactionListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "Id", DbName = "stkxCashTransaction.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "Name", DbName = "stkxCashTransaction.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "Notes", DbName = "stkxCashTransaction.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "SspTransactionId", DbName = "stkxCashTransaction.SspTransactionId", QueryName = "SspTransactionId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "SsPortfolioId", DbName = "stkxCashTransaction.SsPortfolioId", QueryName = "SsPortfolioId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "Source", DbName = "stkxCashTransaction.Source", QueryName = "Source", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "SourceDate", DbName = "stkxCashTransaction.SourceDate", QueryName = "SourceDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "TransactionDate", DbName = "stkxCashTransaction.TransactionDate", QueryName = "TransactionDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "Value", DbName = "stkxCashTransaction.Value", QueryName = "Value", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "TransactionTypeId", DbName = "stkxCashTransaction.TransactionTypeId", QueryName = "TransactionTypeId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CashTransaction", FieldName = "ExpensesTypeId", DbName = "stkxCashTransaction.ExpensesTypeId", QueryName = "ExpensesTypeId", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "SspTransaction", FieldName = "Name", DbName = "stkxSspTransaction.Name", QueryName = "SspTransactionName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Name", DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "TransactionType", FieldName = "Name", DbName = "stkxTransactionType.Name", QueryName = "TransactionTypeName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ExpensesType", FieldName = "Name", DbName = "stkxExpensesType.Name", QueryName = "ExpensesTypeName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxCashTransaction ");

           sql.AppendLine($"   Left Join stkxSspTransaction on stkxCashTransaction.SspTransactionId = stkxSspTransaction.Id");
sql.AppendLine($"   Left Join stkxSsPortfolio on stkxCashTransaction.SsPortfolioId = stkxSsPortfolio.Id");
sql.AppendLine($"   Left Join stkxTransactionType on stkxCashTransaction.TransactionTypeId = stkxTransactionType.Id");
sql.AppendLine($"   Left Join stkxExpensesType on stkxCashTransaction.ExpensesTypeId = stkxExpensesType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<CashTransactionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxCashTransaction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxCashTransaction.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxCashTransaction.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.SspTransactionId.HasValue)
                {
                    sql.AppendLine("stkxCashTransaction.SspTransactionId = @SspTransactionId");
                    parameters.Add("@SspTransactionId", filter.SspTransactionId.Value);
                }
if (filter.SsPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxCashTransaction.SsPortfolioId = @SsPortfolioId");
                    parameters.Add("@SsPortfolioId", filter.SsPortfolioId.Value);
                }
if (filter.TransactionTypeId.HasValue)
                {
                    sql.AppendLine("stkxCashTransaction.TransactionTypeId = @TransactionTypeId");
                    parameters.Add("@TransactionTypeId", filter.TransactionTypeId.Value);
                }
if (filter.ExpensesTypeId.HasValue)
                {
                    sql.AppendLine("stkxCashTransaction.ExpensesTypeId = @ExpensesTypeId");
                    parameters.Add("@ExpensesTypeId", filter.ExpensesTypeId.Value);
                }

                if (filter.SourceDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCashTransaction.SourceDate >= @SourceDateFrom");
                    parameters.Add("@SourceDateFrom", filter.SourceDate.From.Value);
                }
                if (filter.SourceDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCashTransaction.SourceDate <= @SourceDateTo");
                    parameters.Add("@SourceDateTo", filter.SourceDate.To.Value);
                }
if (filter.TransactionDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCashTransaction.TransactionDate >= @TransactionDateFrom");
                    parameters.Add("@TransactionDateFrom", filter.TransactionDate.From.Value);
                }
                if (filter.TransactionDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCashTransaction.TransactionDate <= @TransactionDateTo");
                    parameters.Add("@TransactionDateTo", filter.TransactionDate.To.Value);
                }
if (filter.Value?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCashTransaction.Value >= @ValueFrom");
                    parameters.Add("@ValueFrom", filter.Value.From.Value);
                }
                if (filter.Value?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCashTransaction.Value <= @ValueTo");
                    parameters.Add("@ValueTo", filter.Value.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<CashTransactionListFilter> request, DynamicParameters parameters)
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

