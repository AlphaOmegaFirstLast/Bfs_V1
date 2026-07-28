using Bfs.Core.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Stores.Data;
using Bfs.Stores.Data.Interfaces;
using System.Text;

namespace Bfs.Stores.Data.Reports
{
    public class ProductTransactionCompare :QueryBase<ProductTransactionCompareFilter>,  IProductTransactionCompare
    {
        public ProductTransactionCompare(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<ProductTransactionCompareItem>> GetAsync(QueryRequest<ProductTransactionCompareFilter> request)
        {
            var response = new QueryResponse<ProductTransactionCompareItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<ProductTransactionCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<ProductTransactionCompareItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "strProduct.Name", QueryName = "StrProduct_Name", IsAggregare = false });

            //lookups

           //Aggregates
           _fieldList.Add(new QueryField() { DbName = "Sum(strDocumentDetails.quantity)", QueryName = "sumQuantity", IsAggregare = true });

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From strDocumentDetails ");

           sql.AppendLine($"   Left Join strProduct on strDocumentDetails.ProductId = strProduct.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<ProductTransactionCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" strDocumentDetails.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.Quantity))
                {
                    sql.AppendLine("strDocumentDetails.Quantity  = @Quantity ");
                    parameters.Add("@Quantity", filter.Quantity);
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("strProduct.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<ProductTransactionCompareFilter> request, DynamicParameters parameters)
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