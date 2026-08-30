using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Stores.Data.Interfaces;
using Bfs.Stores.Data;
using System.Text;

namespace Bfs.Stores.Data.Lists
{
    public class OperationList: QueryBase<OperationListFilter>,  IOperationList
    {
        public OperationList(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<OperationListItem>> GetAsync(QueryRequest<OperationListFilter> request)
        {
            var response = new QueryResponse<OperationListItem>();

            SetUp(request);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<OperationListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<OperationListItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "strOperation.Id", QueryName = "Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strOperation.Name", QueryName = "Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strOperation.Notes", QueryName = "Notes", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strOperation.EffectTypeId", QueryName = "EffectTypeId", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strOperation.ThirdPartyTypeId", QueryName = "ThirdPartyTypeId", IsAggregare = false });

            //lookups
            _fieldList.Add(new QueryField() { DbName = "strEffectType.Name", QueryName = "EffectTypeName", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "strThirdPartyType.Name", QueryName = "ThirdPartyTypeName", IsAggregare = false });

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From strOperation ");

           sql.AppendLine($"   Left Join strEffectType on strOperation.EffectTypeId = strEffectType.Id");
sql.AppendLine($"   Left Join strThirdPartyType on strOperation.ThirdPartyTypeId = strThirdPartyType.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<OperationListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" strOperation.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("strOperation.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("strOperation.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.EffectTypeId.HasValue)
                {
                    sql.AppendLine("strOperation.EffectTypeId = @EffectTypeId");
                    parameters.Add("@EffectTypeId", filter.EffectTypeId.Value);
                }
if (filter.ThirdPartyTypeId.HasValue)
                {
                    sql.AppendLine("strOperation.ThirdPartyTypeId = @ThirdPartyTypeId");
                    parameters.Add("@ThirdPartyTypeId", filter.ThirdPartyTypeId.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<OperationListFilter> request, DynamicParameters parameters)
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

