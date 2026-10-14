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
    public class SspTransactionList : QueryBase<SspTransactionListFilter>, ISspTransactionList
    {
        private readonly IResourceSecurity? _resourceSecurity;
        //Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public SspTransactionList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<SspTransactionListItem>> GetAsync(QueryRequest<SspTransactionListFilter> request)
        {
            var response = new QueryResponse<SspTransactionListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<SspTransactionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<SspTransactionListItem> DoMapping(IEnumerable<SspTransactionListItem> RecordList)
        {
            return RecordList.Select(record =>
            {
                var item = (SspTransactionListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "Id", DbName = "stkxSspTransaction.Id", QueryName = "Id", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "Notes", DbName = "stkxSspTransaction.Notes", QueryName = "Notes", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "SourceDate", DbName = "stkxSspTransaction.SourceDate", QueryName = "SourceDate", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "TransactionDate", DbName = "stkxSspTransaction.TransactionDate", QueryName = "TransactionDate", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "Source", DbName = "stkxSspTransaction.Source", QueryName = "Source", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "SsPortfolioId", DbName = "stkxSspTransaction.SsPortfolioId", QueryName = "SsPortfolioId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "TransactionTypeId", DbName = "stkxSspTransaction.TransactionTypeId", QueryName = "TransactionTypeId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "Quantity", DbName = "stkxSspTransaction.Quantity", QueryName = "Quantity", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "Price", DbName = "stkxSspTransaction.Price", QueryName = "Price", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "StockShareId", DbName = "stkxSspTransaction.StockShareId", QueryName = "StockShareId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "ToQuantity", DbName = "stkxSspTransaction.ToQuantity", QueryName = "ToQuantity", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "ToPortfolioId", DbName = "stkxSspTransaction.ToPortfolioId", QueryName = "ToPortfolioId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "CurrencyId", DbName = "stkxSspTransaction.CurrencyId", QueryName = "CurrencyId", IsAggregare = false });

            //object fields

            //lookups
            _fieldList.Add(new QueryField() { ComponentName = "TransactionType", FieldName = "Name", DbName = "stkxTransactionType.Name", QueryName = "TransactionTypeName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "StockShare", FieldName = "Name", DbName = "stkxStockShare.Name", QueryName = "StockShareName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "Currency", FieldName = "Name", DbName = "stkxCurrency.Name", QueryName = "CurrencyName", IsAggregare = false });

            //Template_Start_Generated_Ignored_1
/*

_fieldList.Add(new QueryField() { ComponentName = "ToSspTransaction", FieldName = "Name", DbName = "stkxToSspTransaction.Name", QueryName = "ToSspTransactionName", IsAggregare = false });
*/
//Template_End_Generated_Ignored_1

            //autoCompletes
            _fieldList.Add(new QueryField() { ComponentName = "SsPortfolio", FieldName = "Name", DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false });

            //Template_Start_Generated_Ignored_2
/*

_fieldList.Add(new QueryField() { ComponentName = "ToPortfolio", FieldName = "Name", DbName = "stkxToPortfolio.Name", QueryName = "ToPortfolioName", IsAggregare = false });
*/
//Template_End_Generated_Ignored_2

            //Aggregates

            //Template_Start_Code_DontOverwrite_5
_fieldList.Add(new QueryField() { ComponentName = "ToPortfolio", FieldName = "Name", DbName = "toPortfolio.Name", QueryName = "ToPortfolioName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "ToSspTransaction", FieldName = "Name", DbName = "('['+ stkxTransactionType.Name + '] '+ FORMAT(ToSspTransaction.TransactionDate, 'dd-MM-yyyy'))", QueryName = "ToSspTransactionName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "Name", DbName = "('['+ stkxTransactionType.Name + '] '+ FORMAT(stkxSspTransaction.TransactionDate, 'dd-MM-yyyy'))", QueryName = "AutoCompleteName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SspTransaction", FieldName = "Name", DbName = "('['+ stkxTransactionType.Name + '] '+ FORMAT(stkxSspTransaction.TransactionDate, 'dd-MM-yyyy'))", QueryName = "LookupName", IsAggregare = false });
//Template_End_Code_DontOverwrite_5

        }

        protected override string GetFromJoinStatement()
        {
            var sql = new StringBuilder();
            sql.AppendLine(" From stkxSspTransaction ");

            sql.AppendLine($"   Left Join stkxTransactionType on stkxSspTransaction.TransactionTypeId = stkxTransactionType.Id");
            sql.AppendLine($"   Left Join stkxStockShare on stkxSspTransaction.StockShareId = stkxStockShare.Id");
            sql.AppendLine($"   Left Join stkxCurrency on stkxSspTransaction.CurrencyId = stkxCurrency.Id");

            //Template_Start_Generated_Ignored_3
/*

sql.AppendLine($"   Left Join stkxToSspTransaction on stkxSspTransaction.ToSspTransactionId = stkxToSspTransaction.Id");
*/
//Template_End_Generated_Ignored_3

            sql.AppendLine($"   Left Join stkxSsPortfolio on stkxSspTransaction.SsPortfolioId = stkxSsPortfolio.Id");

            //Template_Start_Generated_Ignored_4
/*

sql.AppendLine($"   Left Join stkxToPortfolio on stkxSspTransaction.ToPortfolioId = stkxToPortfolio.Id");
*/
//Template_End_Generated_Ignored_4

            //Template_Start_Code_DontOverwrite_6
sql.AppendLine($"   Left Join stkxSsPortfolio ToPortfolio on stkxSspTransaction.ToPortfolioId = ToPortfolio.Id");
            sql.AppendLine($"   Left Join stkxSspTransaction ToSspTransaction on stkxSspTransaction.ToSspTransactionId = ToSspTransaction.Id");
//Template_End_Code_DontOverwrite_6

            return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<SspTransactionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" stkxSspTransaction.isDeleted=0 ");

            var filter = request.Filter;
            if (filter != null)
            {
                if ((filter.Id.HasValue) && (filter.Id > 0))
                {
                    sql.AppendLine("stkxSspTransaction.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (filter.TransactionTypeId.HasValue)
                {
                    sql.AppendLine("stkxSspTransaction.TransactionTypeId = @TransactionTypeId");
                    parameters.Add("@TransactionTypeId", filter.TransactionTypeId.Value);
                }
                if (filter.StockShareId.HasValue)
                {
                    sql.AppendLine("stkxSspTransaction.StockShareId = @StockShareId");
                    parameters.Add("@StockShareId", filter.StockShareId.Value);
                }
                if (filter.CurrencyId.HasValue)
                {
                    sql.AppendLine("stkxSspTransaction.CurrencyId = @CurrencyId");
                    parameters.Add("@CurrencyId", filter.CurrencyId.Value);
                }
                if (filter.ToSspTransactionId.HasValue)
                {
                    sql.AppendLine("stkxSspTransaction.ToSspTransactionId = @ToSspTransactionId");
                    parameters.Add("@ToSspTransactionId", filter.ToSspTransactionId.Value);
                }

                if (filter.SsPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxSspTransaction.SsPortfolioId = @SsPortfolioId");
                    parameters.Add("@SsPortfolioId", filter.SsPortfolioId.Value);
                }
                if (filter.ToPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxSspTransaction.ToPortfolioId = @ToPortfolioId");
                    parameters.Add("@ToPortfolioId", filter.ToPortfolioId.Value);
                }

                if (filter.SourceDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.SourceDate >= @SourceDateFrom");
                    parameters.Add("@SourceDateFrom", filter.SourceDate.From.Value);
                }
                if (filter.SourceDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.SourceDate <= @SourceDateTo");
                    parameters.Add("@SourceDateTo", filter.SourceDate.To.Value);
                }
                if (filter.TransactionDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.TransactionDate >= @TransactionDateFrom");
                    parameters.Add("@TransactionDateFrom", filter.TransactionDate.From.Value);
                }
                if (filter.TransactionDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.TransactionDate <= @TransactionDateTo");
                    parameters.Add("@TransactionDateTo", filter.TransactionDate.To.Value);
                }
                if (filter.Quantity?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Quantity >= @QuantityFrom");
                    parameters.Add("@QuantityFrom", filter.Quantity.From.Value);
                }
                if (filter.Quantity?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Quantity <= @QuantityTo");
                    parameters.Add("@QuantityTo", filter.Quantity.To.Value);
                }
                if (filter.Price?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Price >= @PriceFrom");
                    parameters.Add("@PriceFrom", filter.Price.From.Value);
                }
                if (filter.Price?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSspTransaction.Price <= @PriceTo");
                    parameters.Add("@PriceTo", filter.Price.To.Value);
                }

                //Template_Start_Code_DontOverwrite_7
//Template_End_Code_DontOverwrite_7

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));
        }

        protected override string GetHavingConditions(QueryRequest<SspTransactionListFilter> request, DynamicParameters parameters)
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