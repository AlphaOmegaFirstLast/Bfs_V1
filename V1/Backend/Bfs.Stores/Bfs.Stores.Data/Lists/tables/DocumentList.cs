using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data;
using System.Text;

namespace Bfs.Stores.Data.Lists
{
    public class DocumentList: QueryBase<DocumentListFilter>,  IDocumentList
    {
        public DocumentList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<DocumentListItem>> GetAsync(QueryRequest<DocumentListFilter> request)
        {
            var response = new QueryResponse<DocumentListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<DocumentListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<DocumentListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "strDocument.Id", QueryName = "Id", IsAggregare = false, ComponentName = "Document"});
_fieldList.Add(new QueryField() { DbName = "strDocument.Name", QueryName = "Name", IsAggregare = false, ComponentName = "Document"});
_fieldList.Add(new QueryField() { DbName = "strDocument.StoreId", QueryName = "StoreId", IsAggregare = false, ComponentName = "Document"});
_fieldList.Add(new QueryField() { DbName = "strDocument.OperationId", QueryName = "OperationId", IsAggregare = false, ComponentName = "Document"});
_fieldList.Add(new QueryField() { DbName = "strDocument.ResponseDate", QueryName = "ResponseDate", IsAggregare = false, ComponentName = "Document"});
_fieldList.Add(new QueryField() { DbName = "strDocument.Notes", QueryName = "Notes", IsAggregare = false, ComponentName = "Document"});

            //lookups
            _fieldList.Add(new QueryField() { DbName = "strStore.Name", QueryName = "StoreName", IsAggregare = false, ComponentName = "Store"});
_fieldList.Add(new QueryField() { DbName = "strOperation.Name", QueryName = "OperationName", IsAggregare = false, ComponentName = "Operation"});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From strDocument ");

           sql.AppendLine($"   Left Join strStore on strDocument.StoreId = strStore.Id");
sql.AppendLine($"   Left Join strOperation on strDocument.OperationId = strOperation.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<DocumentListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" strDocument.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("strDocument.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("strDocument.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.StoreId.HasValue)
                {
                    sql.AppendLine("strDocument.StoreId = @StoreId");
                    parameters.Add("@StoreId", filter.StoreId.Value);
                }
if (filter.OperationId.HasValue)
                {
                    sql.AppendLine("strDocument.OperationId = @OperationId");
                    parameters.Add("@OperationId", filter.OperationId.Value);
                }

                if (filter.ResponseDate?.From.HasValue == true)
                {
                    sql.AppendLine("strDocument.ResponseDate >= @ResponseDateFrom");
                    parameters.Add("@ResponseDateFrom", filter.ResponseDate.From.Value);
                }
                if (filter.ResponseDate?.To.HasValue == true)
                {
                    sql.AppendLine("strDocument.ResponseDate <= @ResponseDateTo");
                    parameters.Add("@ResponseDateTo", filter.ResponseDate.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<DocumentListFilter> request, DynamicParameters parameters)
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

