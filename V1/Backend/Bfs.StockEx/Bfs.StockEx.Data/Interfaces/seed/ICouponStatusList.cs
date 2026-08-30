using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ICouponStatusList
    {
        Task<QueryResponse<CouponStatusListItem>> GetAsync(QueryRequest<CouponStatusListFilter> request);
    }
}

