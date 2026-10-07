using Bfs.Core.Data;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.Auth.Data;
using Bfs.Auth.Data.Interfaces;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.Auth.Data.Reports
{
    public class RoleRepCompare :QueryBase<RoleRepCompareFilter>,  IRoleRepCompare
    {
        private readonly IResourceSecurity _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public RoleRepCompare(string connectionString, IResourceSecurity resourceSecurity
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

        public async Task<QueryResponse<RoleRepCompareItem>> GetAsync(QueryRequest<RoleRepCompareFilter> request)
        {
            var response = new QueryResponse<RoleRepCompareItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<RoleRepCompareItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<RoleRepCompareItem>)items;

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
            _fieldList.Add(new QueryField() { DbName = "athAuthRole.Id", QueryName = "AuthRole_Id", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athAuthRole.Name", QueryName = "AuthRole_Name", IsAggregare = false });
_fieldList.Add(new QueryField() { DbName = "athAuthRole.Notes", QueryName = "AuthRole_Notes", IsAggregare = false });

            //lookups

            //autoComplete

           //Aggregates

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From athAuthRole ");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<RoleRepCompareFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" athAuthRole.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.AuthRole_Id.HasValue) && (filter.AuthRole_Id>0))
                {
                    sql.AppendLine(" AND athAuthRole.Id = @AuthRole_Id");
                    parameters.Add("@AuthRole_Id", filter.AuthRole_Id);
                }

                if (!string.IsNullOrEmpty(filter.AuthRole_Name))
                {
                    sql.AppendLine("athAuthRole.Name like '%'+@AuthRole_Name+'%' ");
                    parameters.Add("@AuthRole_Name", filter.AuthRole_Name);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<RoleRepCompareFilter> request, DynamicParameters parameters)
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
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5     
    }
}