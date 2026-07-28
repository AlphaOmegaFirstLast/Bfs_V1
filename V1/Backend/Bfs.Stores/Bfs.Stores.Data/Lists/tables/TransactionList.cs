using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data;
using System.Text;

namespace Bfs.Stores.Data.Lists
{
    public class TransactionList: QueryBase<TransactionListFilter>,  ITransactionList
    {
        public TransactionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<TransactionListItem>> GetAsync(QueryRequest<TransactionListFilter> request)
        {
            var response = new QueryResponse<TransactionListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<TransactionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<TransactionListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "strTransaction.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strTransaction.Quantity", QueryName = "Quantity", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strTransaction.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strTransaction.StoreId", QueryName = "StoreId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strTransaction.OperationId", QueryName = "OperationId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strTransaction.ProductId", QueryName = "ProductId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "strStore.Name", QueryName = "StoreName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strOperation.Name", QueryName = "OperationName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strProduct.Name", QueryName = "ProductName", IsAggregare = false });

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From strTransaction ");

           sql.AppendLine($"   Left Join strStore on strTransaction.StoreId = strStore.Id");
sql.AppendLine($"   Left Join strOperation on strTransaction.OperationId = strOperation.Id");
sql.AppendLine($"   Left Join strProduct on strTransaction.ProductId = strProduct.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<TransactionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" strTransaction.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("strTransaction.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (filter.StoreId.HasValue)
                {
                    sql.AppendLine("strTransaction.StoreId = @StoreId");
                    parameters.Add("@StoreId", filter.StoreId.Value);
                }
if (filter.OperationId.HasValue)
                {
                    sql.AppendLine("strTransaction.OperationId = @OperationId");
                    parameters.Add("@OperationId", filter.OperationId.Value);
                }
if (filter.ProductId.HasValue)
                {
                    sql.AppendLine("strTransaction.ProductId = @ProductId");
                    parameters.Add("@ProductId", filter.ProductId.Value);
                }

                if (filter.Quantity?.From.HasValue == true)
                {
                    sql.AppendLine("strTransaction.Quantity >= @QuantityFrom");
                    parameters.Add("@QuantityFrom", filter.Quantity.From.Value);
                }
                if (filter.Quantity?.To.HasValue == true)
                {
                    sql.AppendLine("strTransaction.Quantity <= @QuantityTo");
                    parameters.Add("@QuantityTo", filter.Quantity.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<TransactionListFilter> request, DynamicParameters parameters)
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

