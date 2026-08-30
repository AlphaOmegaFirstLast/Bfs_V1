using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ICouponTypeList
    {
        Task<QueryResponse<CouponTypeListItem>> GetAsync(QueryRequest<CouponTypeListFilter> request);
    }
}

