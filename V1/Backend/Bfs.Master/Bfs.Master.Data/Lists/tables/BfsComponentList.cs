using Bfs.Core.Data;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data;
using System.Text;

namespace Bfs.Master.Data.Lists
{
    public class BfsComponentList: QueryBase<BfsComponentListFilter>,  IBfsComponentList
    {
        private readonly IResourceSecurity? _resourceSecurity;

        public BfsComponentList(string connectionString, IResourceSecurity? resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsComponentListItem>> GetAsync(QueryRequest<BfsComponentListFilter> request)
        {
            var response = new QueryResponse<BfsComponentListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsComponentListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BfsComponentListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "Id", DbName = "BfsComponent.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "BfsSystemId", DbName = "BfsComponent.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "IsSoftDelete", DbName = "BfsComponent.IsSoftDelete", QueryName = "IsSoftDelete", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "Name", DbName = "BfsComponent.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "DisplayName", DbName = "BfsComponent.DisplayName", QueryName = "DisplayName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "DataTypeId", DbName = "BfsComponent.DataTypeId", QueryName = "DataTypeId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "MenuName", DbName = "BfsComponent.MenuName", QueryName = "MenuName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "MenuPlaceHolder", DbName = "BfsComponent.MenuPlaceHolder", QueryName = "MenuPlaceHolder", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "QueryBaseTable", DbName = "BfsComponent.QueryBaseTable", QueryName = "QueryBaseTable", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "Notes", DbName = "BfsComponent.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "InterfaceRequired", DbName = "BfsComponent.InterfaceRequired", QueryName = "InterfaceRequired", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "Name", DbName = "BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "DataType", FieldName = "Name", DbName = "DataType.Name", QueryName = "DataTypeName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsComponent ");

           sql.AppendLine($"   Left Join BfsSystem on BfsComponent.BfsSystemId = BfsSystem.Id");
sql.AppendLine($"   Left Join DataType on BfsComponent.DataTypeId = DataType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsComponentListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsComponent.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("BfsComponent.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("BfsComponent.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }
if (!string.IsNullOrEmpty(filter.InterfaceRequired))
                {
                    sql.AppendLine("BfsComponent.InterfaceRequired like '%'+@InterfaceRequired+'%' ");
                    parameters.Add("@InterfaceRequired", filter.InterfaceRequired);
                }

                if (filter.BfsSystemId.HasValue)
                {
                    sql.AppendLine("BfsComponent.BfsSystemId = @BfsSystemId");
                    parameters.Add("@BfsSystemId", filter.BfsSystemId.Value);
                }
if (filter.DataTypeId.HasValue)
                {
                    sql.AppendLine("BfsComponent.DataTypeId = @DataTypeId");
                    parameters.Add("@DataTypeId", filter.DataTypeId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsComponentListFilter> request, DynamicParameters parameters)
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

