using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data;
using System.Text;

namespace Bfs.Stores.Data.Lists
{
    public class DocumentDetailsList: QueryBase<DocumentDetailsListFilter>,  IDocumentDetailsList
    {
        public DocumentDetailsList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<DocumentDetailsListItem>> GetAsync(QueryRequest<DocumentDetailsListFilter> request)
        {
            var response = new QueryResponse<DocumentDetailsListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<DocumentDetailsListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<DocumentDetailsListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "strDocumentDetails.Id", QueryName = "Id", IsAggregare = false, ComponentName = "DocumentDetails"});
_fieldList.Add(new QueryField() { DbName = "strDocumentDetails.Quantity", QueryName = "Quantity", IsAggregare = false, ComponentName = "DocumentDetails"});
_fieldList.Add(new QueryField() { DbName = "strDocumentDetails.Notes", QueryName = "Notes", IsAggregare = false, ComponentName = "DocumentDetails"});
_fieldList.Add(new QueryField() { DbName = "strDocumentDetails.ProductId", QueryName = "ProductId", IsAggregare = false, ComponentName = "DocumentDetails"});
_fieldList.Add(new QueryField() { DbName = "strDocumentDetails.UnitId", QueryName = "UnitId", IsAggregare = false, ComponentName = "DocumentDetails"});
_fieldList.Add(new QueryField() { DbName = "strDocumentDetails.DocumentId", QueryName = "DocumentId", IsAggregare = false, ComponentName = "DocumentDetails"});

            //lookups
            _fieldList.Add(new QueryField() { DbName = "strProduct.Name", QueryName = "ProductName", IsAggregare = false, ComponentName = "Product"});
_fieldList.Add(new QueryField() { DbName = "strUnit.Name", QueryName = "UnitName", IsAggregare = false, ComponentName = "Unit"});
_fieldList.Add(new QueryField() { DbName = "strDocument.Name", QueryName = "DocumentName", IsAggregare = false, ComponentName = "Document"});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From strDocumentDetails ");

           sql.AppendLine($"   Left Join strProduct on strDocumentDetails.ProductId = strProduct.Id");
sql.AppendLine($"   Left Join strUnit on strDocumentDetails.UnitId = strUnit.Id");
sql.AppendLine($"   Left Join strDocument on strDocumentDetails.DocumentId = strDocument.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<DocumentDetailsListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" strDocumentDetails.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("strDocumentDetails.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (filter.ProductId.HasValue)
                {
                    sql.AppendLine("strDocumentDetails.ProductId = @ProductId");
                    parameters.Add("@ProductId", filter.ProductId.Value);
                }
if (filter.UnitId.HasValue)
                {
                    sql.AppendLine("strDocumentDetails.UnitId = @UnitId");
                    parameters.Add("@UnitId", filter.UnitId.Value);
                }
if (filter.DocumentId.HasValue)
                {
                    sql.AppendLine("strDocumentDetails.DocumentId = @DocumentId");
                    parameters.Add("@DocumentId", filter.DocumentId.Value);
                }

                if (filter.Quantity?.From.HasValue == true)
                {
                    sql.AppendLine("strDocumentDetails.Quantity >= @QuantityFrom");
                    parameters.Add("@QuantityFrom", filter.Quantity.From.Value);
                }
                if (filter.Quantity?.To.HasValue == true)
                {
                    sql.AppendLine("strDocumentDetails.Quantity <= @QuantityTo");
                    parameters.Add("@QuantityTo", filter.Quantity.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<DocumentDetailsListFilter> request, DynamicParameters parameters)
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

