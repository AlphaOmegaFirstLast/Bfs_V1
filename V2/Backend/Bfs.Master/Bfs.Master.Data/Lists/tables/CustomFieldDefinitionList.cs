using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Master.Data.Interfaces;
using Bfs.Master.Data;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Lists
{
    public class CustomFieldDefinitionList: QueryBase<CustomFieldDefinitionListFilter>,  ICustomFieldDefinitionList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public CustomFieldDefinitionList(string connectionString, IResourceSecurity? resourceSecurity
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

        )
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

        }

        private readonly string _connectionString;

        public async Task<QueryResponse<CustomFieldDefinitionListItem>> GetAsync(QueryRequest<CustomFieldDefinitionListFilter> request)
        {
            var response = new QueryResponse<CustomFieldDefinitionListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<CustomFieldDefinitionListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<CustomFieldDefinitionListItem> DoMapping(IEnumerable<CustomFieldDefinitionListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (CustomFieldDefinitionListItem)record;
//object fields are stored as JSON in the database, so we need to parse them into the correct type
                item.FieldValidation = SerializationHelper.GetParsed<CustomFieldDefinitionListItem, FieldValidation>(item, "JsonFieldValidation");

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "CustomFieldDefinition", FieldName = "Id", DbName = "CustomFieldDefinition.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomFieldDefinition", FieldName = "Name", DbName = "CustomFieldDefinition.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomFieldDefinition", FieldName = "Notes", DbName = "CustomFieldDefinition.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomFieldDefinition", FieldName = "DisplayName", DbName = "CustomFieldDefinition.DisplayName", QueryName = "DisplayName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CustomFieldDefinition", FieldName = "BfsComponentId", DbName = "CustomFieldDefinition.BfsComponentId", QueryName = "BfsComponentId", IsAggregare = false});

            //object fields
            _fieldList.Add(new QueryField() {ComponentName = "CustomFieldDefinition", FieldName = "FieldValidation", DbName = "CustomFieldDefinition.FieldValidation", QueryName = "JsonFieldValidation", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "BfsComponent", FieldName = "Name", DbName = "BfsComponent.Name", QueryName = "BfsComponentName", IsAggregare = false});

            //autoCompletes

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
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("CustomFieldDefinition.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

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
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

    }
}