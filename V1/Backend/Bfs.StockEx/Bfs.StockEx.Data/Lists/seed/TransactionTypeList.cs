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
    public class TransactionTypeList : QueryBase<TransactionTypeListFilter>, ITransactionTypeList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public TransactionTypeList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<TransactionTypeListItem>> GetAsync(QueryRequest<TransactionTypeListFilter> request)
        {
            var response = new QueryResponse<TransactionTypeListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<TransactionTypeListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<TransactionTypeListItem>)items;

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
            _fieldList.Add(new QueryField() { ComponentName = "TransactionType", FieldName = "Id", DbName = "stkxTransactionType.Id", QueryName = "Id", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "TransactionType", FieldName = "Name", DbName = "stkxTransactionType.Name", QueryName = "Name", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "TransactionType", FieldName = "Notes", DbName = "stkxTransactionType.Notes", QueryName = "Notes", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "TransactionType", FieldName = "EffectTypeId", DbName = "stkxTransactionType.EffectTypeId", QueryName = "EffectTypeId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "TransactionType", FieldName = "StockEntityTypeId", DbName = "stkxTransactionType.StockEntityTypeId", QueryName = "StockEntityTypeId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "TransactionType", FieldName = "CalculationMethodId", DbName = "stkxTransactionType.CalculationMethodId", QueryName = "CalculationMethodId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "TransactionType", FieldName = "SourceTypeId", DbName = "stkxTransactionType.SourceTypeId", QueryName = "SourceTypeId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "TransactionType", FieldName = "StockFieldTypeId", DbName = "stkxTransactionType.StockFieldTypeId", QueryName = "StockFieldTypeId", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "TransactionType", FieldName = "NextTransactionTypeId", DbName = "asNextTransactionType.Id", QueryName = "NextTransactionTypeId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { ComponentName = "EffectType", FieldName = "Name", DbName = "stkxEffectType.Name", QueryName = "EffectTypeName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "StockEntityType", FieldName = "Name", DbName = "stkxStockEntityType.Name", QueryName = "StockEntityTypeName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "CalculationMethod", FieldName = "Name", DbName = "stkxCalculationMethod.Name", QueryName = "CalculationMethodName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "SourceType", FieldName = "Name", DbName = "stkxSourceType.Name", QueryName = "SourceTypeName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "StockFieldType", FieldName = "Name", DbName = "stkxStockFieldType.Name", QueryName = "StockFieldTypeName", IsAggregare = false });
            _fieldList.Add(new QueryField() { ComponentName = "asNextTransactionType", FieldName = "Name", DbName = "asNextTransactionType.Name", QueryName = "NextTransactionTypeName", IsAggregare = false });

            //autoCompletes

            //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
            var sql = new StringBuilder();
            sql.AppendLine(" From stkxTransactionType ");

            sql.AppendLine($"   Left Join stkxEffectType on stkxTransactionType.EffectTypeId = stkxEffectType.Id");
            sql.AppendLine($"   Left Join stkxStockEntityType on stkxTransactionType.StockEntityTypeId = stkxStockEntityType.Id");
            sql.AppendLine($"   Left Join stkxCalculationMethod on stkxTransactionType.CalculationMethodId = stkxCalculationMethod.Id");
            sql.AppendLine($"   Left Join stkxSourceType on stkxTransactionType.SourceTypeId = stkxSourceType.Id");
            sql.AppendLine($"   Left Join stkxStockFieldType on stkxTransactionType.StockFieldTypeId = stkxStockFieldType.Id");
            sql.AppendLine($"   Left Join stkxTransactionType  asNextTransactionType on stkxTransactionType.NextTransactionTypeId = asNextTransactionType.Id");

            return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<TransactionTypeListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" stkxTransactionType.isDeleted=0 ");

            var filter = request.Filter;
            if (filter != null)
            {
                if ((filter.Id.HasValue) && (filter.Id > 0))
                {
                    sql.AppendLine("stkxTransactionType.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxTransactionType.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.EffectTypeId.HasValue)
                {
                    sql.AppendLine("stkxTransactionType.EffectTypeId = @EffectTypeId");
                    parameters.Add("@EffectTypeId", filter.EffectTypeId.Value);
                }
                if (filter.StockEntityTypeId.HasValue)
                {
                    sql.AppendLine("stkxTransactionType.StockEntityTypeId = @StockEntityTypeId");
                    parameters.Add("@StockEntityTypeId", filter.StockEntityTypeId.Value);
                }
                if (filter.CalculationMethodId.HasValue)
                {
                    sql.AppendLine("stkxTransactionType.CalculationMethodId = @CalculationMethodId");
                    parameters.Add("@CalculationMethodId", filter.CalculationMethodId.Value);
                }
                if (filter.SourceTypeId.HasValue)
                {
                    sql.AppendLine("stkxTransactionType.SourceTypeId = @SourceTypeId");
                    parameters.Add("@SourceTypeId", filter.SourceTypeId.Value);
                }
                if (filter.StockFieldTypeId.HasValue)
                {
                    sql.AppendLine("stkxTransactionType.StockFieldTypeId = @StockFieldTypeId");
                    parameters.Add("@StockFieldTypeId", filter.StockFieldTypeId.Value);
                }
                if (filter.NextTransactionTypeId.HasValue)
                {
                    sql.AppendLine("stkxTransactionType.NextTransactionTypeId = @NextTransactionTypeId");
                    parameters.Add("@NextTransactionTypeId", filter.NextTransactionTypeId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));
        }

        protected override string GetHavingConditions(QueryRequest<TransactionTypeListFilter> request, DynamicParameters parameters)
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

