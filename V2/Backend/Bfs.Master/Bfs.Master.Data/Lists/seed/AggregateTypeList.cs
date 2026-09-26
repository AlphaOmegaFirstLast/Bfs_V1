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
    public class AggregateTypeList: QueryBase<AggregateTypeListFilter>,  IAggregateTypeList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public AggregateTypeList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<AggregateTypeListItem>> GetAsync(QueryRequest<AggregateTypeListFilter> request)
        {
            var response = new QueryResponse<AggregateTypeListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<AggregateTypeListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<AggregateTypeListItem> DoMapping(IEnumerable<AggregateTypeListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (AggregateTypeListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "AggregateType", FieldName = "Id", DbName = "AggregateType.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "AggregateType", FieldName = "Name", DbName = "AggregateType.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "AggregateType", FieldName = "Notes", DbName = "AggregateType.Notes", QueryName = "Notes", IsAggregare = false});

            //object fields

            //lookups

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From AggregateType ");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<AggregateTypeListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" AggregateType.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("AggregateType.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("AggregateType.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<AggregateTypeListFilter> request, DynamicParameters parameters)
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