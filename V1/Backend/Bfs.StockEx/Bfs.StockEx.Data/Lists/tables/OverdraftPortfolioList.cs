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
    public class OverdraftPortfolioList: QueryBase<OverdraftPortfolioListFilter>,  IOverdraftPortfolioList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public OverdraftPortfolioList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<OverdraftPortfolioListItem>> GetAsync(QueryRequest<OverdraftPortfolioListFilter> request)
        {
            var response = new QueryResponse<OverdraftPortfolioListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<OverdraftPortfolioListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<OverdraftPortfolioListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "Id", DbName = "stkxOverdraftPortfolio.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "Name", DbName = "stkxOverdraftPortfolio.Name", QueryName = "Name", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "Notes", DbName = "stkxOverdraftPortfolio.Notes", QueryName = "Notes", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "SsPortfolioId", DbName = "stkxOverdraftPortfolio.SsPortfolioId", QueryName = "SsPortfolioId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "OverdraftPortfolio", FieldName = "OverdraftValue", DbName = "stkxOverdraftPortfolio.OverdraftValue", QueryName = "OverdraftValue", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "SsPortfolio", FieldName = "Name", DbName = "stkxSsPortfolio.Name", QueryName = "SsPortfolioName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxOverdraftPortfolio ");

           sql.AppendLine($"   Left Join stkxSsPortfolio on stkxOverdraftPortfolio.SsPortfolioId = stkxSsPortfolio.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<OverdraftPortfolioListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxOverdraftPortfolio.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxOverdraftPortfolio.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxOverdraftPortfolio.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.SsPortfolioId.HasValue)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.SsPortfolioId = @SsPortfolioId");
                    parameters.Add("@SsPortfolioId", filter.SsPortfolioId.Value);
                }

                if (filter.OverdraftValue?.From.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.OverdraftValue >= @OverdraftValueFrom");
                    parameters.Add("@OverdraftValueFrom", filter.OverdraftValue.From.Value);
                }
                if (filter.OverdraftValue?.To.HasValue == true)
                {
                    sql.AppendLine("stkxOverdraftPortfolio.OverdraftValue <= @OverdraftValueTo");
                    parameters.Add("@OverdraftValueTo", filter.OverdraftValue.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<OverdraftPortfolioListFilter> request, DynamicParameters parameters)
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