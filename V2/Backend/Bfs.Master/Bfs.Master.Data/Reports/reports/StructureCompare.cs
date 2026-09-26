using Bfs.Core.Data;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Master.Data;
using Bfs.Master.Data.Interfaces;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Master.Data.Reports
{
    public class StructureCompare :QueryBase<StructureCompareFilter>,  IStructureCompare
    {
        private readonly IResourceSecurity _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public StructureCompare(string connectionString, IResourceSecurity resourceSecurity
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

        public async Task<QueryResponse<StructureCompareItem>> GetAsync(QueryRequest<StructureCompareFilter> request)
        {
            var response = new QueryResponse<StructureCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<StructureCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<StructureCompareItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "BfsComponent.DataTypeId", QueryName = "BfsComponent_DataTypeId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "BfsComponent.DisplayName", QueryName = "BfsComponent_DisplayName", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "DataType.Name", QueryName = "DataTypeName", IsAggregare = false });

            //autoComplete

           //Aggregates
           _fieldList.Add(new QueryField() { DbName = "Count(BfsComponent.id)", QueryName = "countId", IsAggregare = true });

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From BfsField ");

           sql.AppendLine($"   Left Join BfsComponent on BfsField.BfsComponentId = BfsComponent.Id");

           sql.AppendLine($"   Left Join DataType on BfsComponent.DataTypeId = DataType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<StructureCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" BfsField.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {

                if (!string.IsNullOrEmpty(filter.BfsComponent_DisplayName))
                {
                    sql.AppendLine("BfsComponent.DisplayName like '%'+@BfsComponent_DisplayName+'%' ");
                    parameters.Add("@BfsComponent_DisplayName", filter.BfsComponent_DisplayName);
                }

                if (filter.BfsComponent_DataTypeId.HasValue)
                {
                    sql.AppendLine("BfsComponent.DataTypeId = @BfsComponent_DataTypeId");
                    parameters.Add("@BfsComponent_DataTypeId", filter.BfsComponent_DataTypeId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<StructureCompareFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            if (filter.countId?.From.HasValue == true)
            {
                sql.AppendLine("Count(BfsComponent.Id) >= @countIdFrom");
                parameters.Add("@countIdFrom", filter.countId.From.Value);
            }
            if (filter.countId?.To.HasValue == true)
            {
                sql.AppendLine("Count(BfsComponent.Id) <= @countIdTo");
                parameters.Add("@countIdTo", filter.countId.To.Value);
            }

            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
       } 
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5

    }
}