using Bfs.Core.Data;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data;
using System.Text;

namespace Bfs.Stores.Data.Lists
{
    public class StoreList: QueryBase<StoreListFilter>,  IStoreList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public StoreList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity ?? throw new ArgumentNullException(nameof(resourceSecurity));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<StoreListItem>> GetAsync(QueryRequest<StoreListFilter> request)
        {
            var response = new QueryResponse<StoreListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<StoreListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<StoreListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "Store", FieldName = "Id", DbName = "strStore.Id", QueryName = "Store_Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Store", FieldName = "Name", DbName = "strStore.Name", QueryName = "Store_Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Store", FieldName = "Notes", DbName = "strStore.Notes", QueryName = "Store_Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Store", FieldName = "AreaId", DbName = "strStore.AreaId", QueryName = "Store_AreaId", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "Area", FieldName = "[Name]", DbName = "strArea.Name", QueryName = "AreaName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From strStore ");

           sql.AppendLine($"   Left Join strArea on strStore.AreaId = strArea.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<StoreListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" strStore.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("strStore.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("strStore.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.AreaId.HasValue)
                {
                    sql.AppendLine("strStore.AreaId = @AreaId");
                    parameters.Add("@AreaId", filter.AreaId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<StoreListFilter> request, DynamicParameters parameters)
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

