using Bfs.Core.Data;
using Bfs.Core.Helpers;
using Bfs.Core.ObjectFields;
using Bfs.Core.Services.Security;

using Dapper;
using Microsoft.Data.SqlClient;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Data;
using System.Text;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Lists
{
    public class CurrentPriceList: QueryBase<CurrentPriceListFilter>,  ICurrentPriceList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public CurrentPriceList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<CurrentPriceListItem>> GetAsync(QueryRequest<CurrentPriceListFilter> request)
        {
            var response = new QueryResponse<CurrentPriceListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<CurrentPriceListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<CurrentPriceListItem> DoMapping(IEnumerable<CurrentPriceListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (CurrentPriceListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "CurrentPrice", FieldName = "Id", DbName = "stkxCurrentPrice.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CurrentPrice", FieldName = "StockShareId", DbName = "stkxCurrentPrice.StockShareId", QueryName = "StockShareId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CurrentPrice", FieldName = "TransactionDate", DbName = "stkxCurrentPrice.TransactionDate", QueryName = "TransactionDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CurrentPrice", FieldName = "Price", DbName = "stkxCurrentPrice.Price", QueryName = "Price", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "StockShare", FieldName = "Name", DbName = "stkxStockShare.Name", QueryName = "StockShareName", IsAggregare = false});

            //autoCompletes

           //Aggregates

//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxCurrentPrice ");

           sql.AppendLine($"   Left Join stkxStockShare on stkxCurrentPrice.StockShareId = stkxStockShare.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<CurrentPriceListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxCurrentPrice.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxCurrentPrice.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                   if (filter.IsAutoComplete.HasValue && filter.IsAutoComplete.Value)
                   {
                      sql.AppendLine("stkxCurrentPrice.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);                    
                   }
                   else
                   {
                      sql.AppendLine("stkxCurrentPrice.Name like '%'+@Name+'%' ");
                      parameters.Add("@Name", filter.Name);
                   }
                }

                if (filter.StockShareId.HasValue)
                {
                    sql.AppendLine("stkxCurrentPrice.StockShareId = @StockShareId");
                    parameters.Add("@StockShareId", filter.StockShareId.Value);
                }

                if (filter.TransactionDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCurrentPrice.TransactionDate >= @TransactionDateFrom");
                    parameters.Add("@TransactionDateFrom", filter.TransactionDate.From.Value);
                }
                if (filter.TransactionDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCurrentPrice.TransactionDate <= @TransactionDateTo");
                    parameters.Add("@TransactionDateTo", filter.TransactionDate.To.Value);
                }
if (filter.Price?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCurrentPrice.Price >= @PriceFrom");
                    parameters.Add("@PriceFrom", filter.Price.From.Value);
                }
                if (filter.Price?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCurrentPrice.Price <= @PriceTo");
                    parameters.Add("@PriceTo", filter.Price.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<CurrentPriceListFilter> request, DynamicParameters parameters)
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