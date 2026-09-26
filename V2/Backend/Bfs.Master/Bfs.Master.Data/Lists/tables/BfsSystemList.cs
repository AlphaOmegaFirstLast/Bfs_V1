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
    public class BfsSystemList: QueryBase<BfsSystemListFilter>,  IBfsSystemList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public BfsSystemList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<BfsSystemListItem>> GetAsync(QueryRequest<BfsSystemListFilter> request)
        {
            var response = new QueryResponse<BfsSystemListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<BfsSystemListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<BfsSystemListItem> DoMapping(IEnumerable<BfsSystemListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (BfsSystemListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "Id", DbName = "BfsSystem.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "IsMaster", DbName = "BfsSystem.IsMaster", QueryName = "IsMaster", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "Notes", DbName = "BfsSystem.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "SystemTemplateId", DbName = "BfsSystem.SystemTemplateId", QueryName = "SystemTemplateId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "BasePortNumber", DbName = "BfsSystem.BasePortNumber", QueryName = "BasePortNumber", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "DbPrefix", DbName = "BfsSystem.DbPrefix", QueryName = "DbPrefix", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "Logo", DbName = "BfsSystem.Logo", QueryName = "Logo", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "BfsSystem", FieldName = "Name", DbName = "BfsSystem.Name", QueryName = "Name", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "SystemTemplate", FieldName = "Name", DbName = "SystemTemplate.Name", QueryName = "SystemTemplateName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsSystem ");

           sql.AppendLine($"   Left Join SystemTemplate on BfsSystem.SystemTemplateId = SystemTemplate.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<BfsSystemListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsSystem.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("BfsSystem.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Logo))
                {
                    sql.AppendLine("BfsSystem.Logo like '%'+@Logo+'%' ");
                    parameters.Add("@Logo", filter.Logo);
                }
if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("BfsSystem.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.SystemTemplateId.HasValue)
                {
                    sql.AppendLine("BfsSystem.SystemTemplateId = @SystemTemplateId");
                    parameters.Add("@SystemTemplateId", filter.SystemTemplateId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<BfsSystemListFilter> request, DynamicParameters parameters)
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