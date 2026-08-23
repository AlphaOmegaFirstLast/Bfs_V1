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
    public class CouponList: QueryBase<CouponListFilter>,  ICouponList
    {
        private readonly IResourceSecurity _resourceSecurity;

        public CouponList(string connectionString, IResourceSecurity resourceSecurity)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _resourceSecurity = resourceSecurity;
        }

        private readonly string _connectionString;

        public async Task<QueryResponse<CouponListItem>> GetAsync(QueryRequest<CouponListFilter> request)
        {
            var response = new QueryResponse<CouponListItem>();

            await SetUp(request, _resourceSecurity);

            using var db = new SqlConnection(_connectionString);
            {
                // Run Report
                var mainQuery = GetMainSqlStatement();
                var items = await db.QueryAsync<CouponListItem>(mainQuery.sql, mainQuery.parameters);
                response.Items = (List<CouponListItem>)items;

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
            _fieldList.Add(new QueryField() {ComponentName = "Coupon", FieldName = "Id", DbName = "stkxCoupon.Id", QueryName = "Id", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Coupon", FieldName = "StockShareId", DbName = "stkxCoupon.StockShareId", QueryName = "StockShareId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Coupon", FieldName = "CouponTypeId", DbName = "stkxCoupon.CouponTypeId", QueryName = "CouponTypeId", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Coupon", FieldName = "Value", DbName = "stkxCoupon.Value", QueryName = "Value", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Coupon", FieldName = "AnnounceDate", DbName = "stkxCoupon.AnnounceDate", QueryName = "AnnounceDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Coupon", FieldName = "ValueDate", DbName = "stkxCoupon.ValueDate", QueryName = "ValueDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Coupon", FieldName = "DueDate", DbName = "stkxCoupon.DueDate", QueryName = "DueDate", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "Coupon", FieldName = "CouponPercent", DbName = "stkxCoupon.CouponPercent", QueryName = "CouponPercent", IsAggregare = false});

            //lookups
            _fieldList.Add(new QueryField() {ComponentName = "TradingRoom", FieldName = "Name", DbName = "stkxTradingRoom.Name", QueryName = "TradingRoomName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "StockShare", FieldName = "Name", DbName = "stkxStockShare.Name", QueryName = "StockShareName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CouponType", FieldName = "Name", DbName = "stkxCouponType.Name", QueryName = "CouponTypeName", IsAggregare = false});
_fieldList.Add(new QueryField() {ComponentName = "CouponStatus", FieldName = "Name", DbName = "stkxCouponStatus.Name", QueryName = "CouponStatusName", IsAggregare = false});

            //autoCompletes

           //Aggregates

        }

        protected override string GetFromJoinStatement()
        {
           var sql = new StringBuilder();  
           sql.AppendLine(" From stkxCoupon ");

           sql.AppendLine($"   Left Join stkxTradingRoom on stkxCoupon.TradingRoomId = stkxTradingRoom.Id");
sql.AppendLine($"   Left Join stkxStockShare on stkxCoupon.StockShareId = stkxStockShare.Id");
sql.AppendLine($"   Left Join stkxCouponType on stkxCoupon.CouponTypeId = stkxCouponType.Id");
sql.AppendLine($"   Left Join stkxCouponStatus on stkxCoupon.CouponStatusId = stkxCouponStatus.Id");

           return sql.ToString();
        }

        protected override string GetWhereConditions(QueryRequest<CouponListFilter> request, DynamicParameters parameters)
        {
            var sql = new StringBuilder() ;
            sql.AppendLine(" stkxCoupon.isDeleted=0 ");

                         var filter = request.Filter;
            if (filter != null)
            {
            if ((filter.Id.HasValue) && (filter.Id>0))
                {
                    sql.AppendLine("stkxCoupon.Id = @Id");
                    parameters.Add("@Id", filter.Id);
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    sql.AppendLine("stkxCoupon.Name like '%'+@Name+'%' ");
                    parameters.Add("@Name", filter.Name);
                }

                if (filter.TradingRoomId.HasValue)
                {
                    sql.AppendLine("stkxCoupon.TradingRoomId = @TradingRoomId");
                    parameters.Add("@TradingRoomId", filter.TradingRoomId.Value);
                }
if (filter.StockShareId.HasValue)
                {
                    sql.AppendLine("stkxCoupon.StockShareId = @StockShareId");
                    parameters.Add("@StockShareId", filter.StockShareId.Value);
                }
if (filter.CouponTypeId.HasValue)
                {
                    sql.AppendLine("stkxCoupon.CouponTypeId = @CouponTypeId");
                    parameters.Add("@CouponTypeId", filter.CouponTypeId.Value);
                }
if (filter.CouponStatusId.HasValue)
                {
                    sql.AppendLine("stkxCoupon.CouponStatusId = @CouponStatusId");
                    parameters.Add("@CouponStatusId", filter.CouponStatusId.Value);
                }

                if (filter.Value?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCoupon.Value >= @ValueFrom");
                    parameters.Add("@ValueFrom", filter.Value.From.Value);
                }
                if (filter.Value?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCoupon.Value <= @ValueTo");
                    parameters.Add("@ValueTo", filter.Value.To.Value);
                }
if (filter.AnnounceDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCoupon.AnnounceDate >= @AnnounceDateFrom");
                    parameters.Add("@AnnounceDateFrom", filter.AnnounceDate.From.Value);
                }
                if (filter.AnnounceDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCoupon.AnnounceDate <= @AnnounceDateTo");
                    parameters.Add("@AnnounceDateTo", filter.AnnounceDate.To.Value);
                }
if (filter.ValueDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCoupon.ValueDate >= @ValueDateFrom");
                    parameters.Add("@ValueDateFrom", filter.ValueDate.From.Value);
                }
                if (filter.ValueDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCoupon.ValueDate <= @ValueDateTo");
                    parameters.Add("@ValueDateTo", filter.ValueDate.To.Value);
                }
if (filter.DueDate?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCoupon.DueDate >= @DueDateFrom");
                    parameters.Add("@DueDateFrom", filter.DueDate.From.Value);
                }
                if (filter.DueDate?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCoupon.DueDate <= @DueDateTo");
                    parameters.Add("@DueDateTo", filter.DueDate.To.Value);
                }
if (filter.CouponPercent?.From.HasValue == true)
                {
                    sql.AppendLine("stkxCoupon.CouponPercent >= @CouponPercentFrom");
                    parameters.Add("@CouponPercentFrom", filter.CouponPercent.From.Value);
                }
                if (filter.CouponPercent?.To.HasValue == true)
                {
                    sql.AppendLine("stkxCoupon.CouponPercent <= @CouponPercentTo");
                    parameters.Add("@CouponPercentTo", filter.CouponPercent.To.Value);
                }

            }
            return string.Join(" And ", sql.ToString()
                                 .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => s.Trim()));        
        }

        protected override string GetHavingConditions(QueryRequest<CouponListFilter> request, DynamicParameters parameters)
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

