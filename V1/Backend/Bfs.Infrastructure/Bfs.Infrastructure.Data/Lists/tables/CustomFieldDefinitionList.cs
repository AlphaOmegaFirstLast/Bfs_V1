using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Infrastructure.Data.Interfaces;
using Bfs.Infrastructure.Data;
using System.Text;

namespace Bfs.Infrastructure.Data.Lists
{
    public class CustomFieldDefinitionList: QueryBase<CustomFieldDefinitionListFilter>,  ICustomFieldDefinitionList
    {
        public CustomFieldDefinitionList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<CustomFieldDefinitionListItem>> GetAsync(QueryRequest<CustomFieldDefinitionListFilter> request)
        {
            var response = new QueryResponse<CustomFieldDefinitionListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<CustomFieldDefinitionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<CustomFieldDefinitionListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "CustomFieldDefinition.Id", QueryName = "CustomFieldDefinitionId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomFieldDefinition.Name", QueryName = "CustomFieldDefinitionName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomFieldDefinition.Notes", QueryName = "CustomFieldDefinitionNotes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomFieldDefinition.FieldValidation", QueryName = "CustomFieldDefinitionFieldValidation", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomFieldDefinition.DisplayName", QueryName = "CustomFieldDefinitionDisplayName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "CustomFieldDefinition.BfsComponentId", QueryName = "CustomFieldDefinitionBfsComponentId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false });

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From CustomFieldDefinition ");

           sql.AppendLine($"   Left Join BfsComponent on CustomFieldDefinition.BfsComponentId = BfsComponent.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<CustomFieldDefinitionListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" CustomFieldDefinition.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("CustomFieldDefinition.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.BfsComponentId.HasValue)
                {
                    sql.AppendLine("CustomFieldDefinition.BfsComponentId = @BfsComponentId");
                    parameters.Add("@BfsComponentId", filter.BfsComponentId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<CustomFieldDefinitionListFilter> request, DynamicParameters parameters)
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