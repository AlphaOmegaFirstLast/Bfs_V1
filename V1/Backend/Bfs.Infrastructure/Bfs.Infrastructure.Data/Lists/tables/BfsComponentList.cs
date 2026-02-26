using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class BfsComponentList: QueryBase<BfsComponentListFilter>,  IBfsComponentList
    {
        public BfsComponentList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<BfsComponentListItem>> GetAsync(QueryRequest<BfsComponentListFilter> request)
        {
            var response = new QueryResponse<BfsComponentListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsComponentListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<BfsComponentListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "BfsComponent.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.BfsSystemId", QueryName = "BfsSystemId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.IsSoftDelete", QueryName = "IsSoftDelete", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.Name", QueryName = "Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.DisplayName", QueryName = "DisplayName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.DataTypeId", QueryName = "DataTypeId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.MenuName", QueryName = "MenuName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.MenuPlaceHolder", QueryName = "MenuPlaceHolder", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.QueryBaseTable", QueryName = "QueryBaseTable", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.InterfaceRequired", QueryName = "InterfaceRequired", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "BfsSystem.Name", QueryName = "BfsSystemName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "DataType.Name", QueryName = "DataTypeName", IsAggregare = false });

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