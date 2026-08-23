using Bfs.Core.Data;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data;
using System.Text;

namespace Bfs.StockEx.Data.Lists
{
    public class SsPortfolioBalanceList: QueryBase<SsPortfolioBalanceListFilter>,  ISsPortfolioBalanceList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public SsPortfolioBalanceList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<SsPortfolioBalanceListItem>> GetAsync(QueryRequest<SsPortfolioBalanceListFilter> request)
        {
            var response = new QueryResponse<SsPortfolioBalanceListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<SsPortfolioBalanceListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<SsPortfolioBalanceListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "SsPortfolioBalance", FieldName = "Id", DbName = "stkxSsPortfolioBalance.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolioBalance", FieldName = "Name", DbName = "stkxSsPortfolioBalance.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolioBalance", FieldName = "Notes", DbName = "stkxSsPortfolioBalance.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolioBalance", FieldName = "SsPortfolioId", DbName = "stkxSsPortfolioBalance.SsPortfolioId", QueryName = "SsPortfolioId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "SsPortfolioBalance", FieldName = "Balance", DbName = "stkxSsPortfolioBalance.Balance", QueryName = "Balance", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Name", DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxSsPortfolioBalance ");

           sql.AppendLine($"   Left Join stkxSsPortfolio on stkxSsPortfolioBalance.SsPortfolioId = stkxSsPortfolio.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<SsPortfolioBalanceListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxSsPortfolioBalance.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxSsPortfolioBalance.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxSsPortfolioBalance.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.SsPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxSsPortfolioBalance.SsPortfolioId = @SsPortfolioId");
                    parameters.Add("@SsPortfolioId", filter.SsPortfolioId.Value);
                }

                if (filter.Balance?.From.HasValue == true)
                {
                    sql.AppendLine("stkxSsPortfolioBalance.Balance >= @BalanceFrom");
                    parameters.Add("@BalanceFrom", filter.Balance.From.Value);
                }
                if (filter.Balance?.To.HasValue == true)
                {
                    sql.AppendLine("stkxSsPortfolioBalance.Balance <= @BalanceTo");
                    parameters.Add("@BalanceTo", filter.Balance.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<SsPortfolioBalanceListFilter> request, DynamicParameters parameters)
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

