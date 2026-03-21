using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data;
using System.Text;

namespace Bfs.Stores.Data.Lists
{
    public class StrTransactionList: QueryBase<StrTransactionListFilter>,  IStrTransactionList
    {
        public StrTransactionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<StrTransactionListItem>> GetAsync(QueryRequest<StrTransactionListFilter> request)
        {
            var response = new QueryResponse<StrTransactionListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<StrTransactionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<StrTransactionListItem>)items;

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = db.ExecuteScalar<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() { DbName = "StrTransaction.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "StrTransaction.Quantity", QueryName = "Quantity", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "StrTransaction.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "StrTransaction.StrStoreId", QueryName = "StrStoreId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "StrTransaction.StrOperationId", QueryName = "StrOperationId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "StrTransaction.StrProductId", QueryName = "StrProductId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "StrStore.Name", QueryName = "StrStoreName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "StrOperation.Name", QueryName = "StrOperationName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "StrProduct.Name", QueryName = "StrProductName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From StrTransaction ");

           sql.AppendLine($"   Left Join StrStore on StrTransaction.StrStoreId = StrStore.Id");
sql.AppendLine($"   Left Join StrOperation on StrTransaction.StrOperationId = StrOperation.Id");
sql.AppendLine($"   Left Join StrProduct on StrTransaction.StrProductId = StrProduct.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<StrTransactionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" StrTransaction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (filter.StrStoreId.HasValue)
                {
                    sql.AppendLine("StrTransaction.StrStoreId = @StrStoreId");
                    parameters.Add("@StrStoreId", filter.StrStoreId.Value);
                }
if (filter.StrOperationId.HasValue)
                {
                    sql.AppendLine("StrTransaction.StrOperationId = @StrOperationId");
                    parameters.Add("@StrOperationId", filter.StrOperationId.Value);
                }
if (filter.StrProductId.HasValue)
                {
                    sql.AppendLine("StrTransaction.StrProductId = @StrProductId");
                    parameters.Add("@StrProductId", filter.StrProductId.Value);
                }

                if (filter.Quantity?.From.HasValue == true)
                {
                    sql.AppendLine("StrTransaction.Quantity >= @QuantityFrom");
                    parameters.Add("@QuantityFrom", filter.Quantity.From.Value);
                }
                if (filter.Quantity?.To.HasValue == true)
                {
                    sql.AppendLine("StrTransaction.Quantity <= @QuantityTo");
                    parameters.Add("@QuantityTo", filter.Quantity.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<StrTransactionListFilter> request, DynamicParameters parameters)
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