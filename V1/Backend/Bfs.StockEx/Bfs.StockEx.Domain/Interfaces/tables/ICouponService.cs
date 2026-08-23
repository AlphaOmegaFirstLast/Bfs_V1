using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ICouponService: ICrudService<Coupon>
    {
        Task<Coupon> UploadAsync(Coupon contract);

        Task<QueryResponse<CouponListItem>> ListAsync(QueryRequest<CouponListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

