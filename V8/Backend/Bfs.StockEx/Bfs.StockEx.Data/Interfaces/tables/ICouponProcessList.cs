using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ICouponProcessList
    {
        Task<QueryResponse<CouponProcessListItem>> GetAsync(QueryRequest<CouponProcessListFilter> request);
    }
}