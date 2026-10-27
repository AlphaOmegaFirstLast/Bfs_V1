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
    public class CouponProcessList: QueryBase<CouponProcessListFilter>,  ICouponProcessList
    {
        private readonly IResourceSecurity? _resourceSecurity;
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public CouponProcessList(string connectionString, IResourceSecurity? resourceSecurity
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

        public async Task<QueryResponse<CouponProcessListItem>> GetAsync(QueryRequest<CouponProcessListFilter> request)
        {
            var response = new QueryResponse<CouponProcessListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<CouponProcessListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = DoMapping(items);

                // Run Count
                var countQuery = GetCountSqlStatement();
                response.TotalItems = await db.ExecuteScalarAsync<long>(countQuery.sql, countQuery.parameters);
                response.TotalPages = (long)Math.Ceiling(((decimal)response.TotalItems) / (request.PageSize ?? 1));
            }

            return response;
        }

        private List<CouponProcessListItem> DoMapping(IEnumerable<CouponProcessListItem> RecordList)
        {
            return RecordList.Select(record =>
            { var item = (CouponProcessListItem)record;

                return item;
            }).ToList();
        }

        protected override void SetupFields()
        {
            //base fields
            _fieldList.Add(new QueryField() {ComponentName = "CouponProcess", FieldName = "Id", DbName = "stkxCouponProcess.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CouponProcess", FieldName = "CouponId", DbName = "stkxCouponProcess.CouponId", QueryName = "CouponId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CouponProcess", FieldName = "ProcessDate", DbName = "stkxCouponProcess.ProcessDate", QueryName = "ProcessDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CouponProcess", FieldName = "ProcessStatusId", DbName = "stkxCouponProcess.ProcessStatusId", QueryName = "ProcessStatusId", IsAggregare = false});

            //object fields

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "Coupon", FieldName = "Name", DbName = "stkxCoupon.DueDate", QueryName = "CouponName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "ProcessStatus", FieldName = "Name", DbName = "stkxProcessStatus.Name", QueryName = "ProcessStatusName", IsAggregare = false});

            //autoCompletes

           //Aggregates

//Template_Start_Code_DontOverwrite_5
             _fieldList.Add(new QueryField() {ComponentName = "CouponProcess", FieldName = "Name", DbName = "stkxCouponProcess.Id", QueryName = "AutoCompleteName", IsAggregare = false});
             _fieldList.Add(new QueryField() {ComponentName = "CouponProcess", FieldName = "Name", DbName = "stkxCouponProcess.Id", QueryName = "LookupName", IsAggregare = false});
//Template_End_Code_DontOverwrite_5
        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxCouponProcess ");

           sql.AppendLine($"   Left Join stkxCoupon on stkxCouponProcess.CouponId = stkxCoupon.Id");
sql.AppendLine($"   Left Join stkxProcessStatus on stkxCouponProcess.ProcessStatusId = stkxProcessStatus.Id");

//Template_Start_Code_DontOverwrite_6
//Template_End_Code_DontOverwrite_6

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<CouponProcessListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxCouponProcess.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxCouponProcess.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (filter.CouponId.HasValue)
                {
                    sql.AppendLine("stkxCouponProcess.CouponId = @CouponId");
                    parameters.Add("@CouponId", filter.CouponId.Value);
                }
if (filter.ProcessStatusId.HasValue)
                {
                    sql.AppendLine("stkxCouponProcess.ProcessStatusId = @ProcessStatusId");
                    parameters.Add("@ProcessStatusId", filter.ProcessStatusId.Value);
                }

                if (filter.ProcessDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCouponProcess.ProcessDate >= @ProcessDateFrom");
                    parameters.Add("@ProcessDateFrom", filter.ProcessDate.From.Value);
                }
                if (filter.ProcessDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCouponProcess.ProcessDate <= @ProcessDateTo");
                    parameters.Add("@ProcessDateTo", filter.ProcessDate.To.Value);
                }

                //Template_Start_Code_DontOverwrite_7
                //Template_End_Code_DontOverwrite_7      
            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<CouponProcessListFilter> request, DynamicParameters parameters)
        {
            var filter = request.Filter;
            if (filter == null)
            {
                return "";
            }

            var sql = new StringBuilder();

            //Template_Start_Code_DontOverwrite_8
            //Template_End_Code_DontOverwrite_8    
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
       } 
//Template_Start_Code_DontOverwrite_9
//Template_End_Code_DontOverwrite_9   
    }
}