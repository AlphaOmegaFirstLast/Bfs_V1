using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ICouponService: ICrudService<Coupon>
    {
        Task<Coupon> UploadAsync(Coupon contract);

        Task<QueryResponse<CouponListItem>> ListAsync(QueryRequest<CouponListFilter> contractRequest);

//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}
