using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ICouponList
    {
        Task<QueryResponse<CouponListItem>> GetAsync(QueryRequest<CouponListFilter> request);
    }
}

