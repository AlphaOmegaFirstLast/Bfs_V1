using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ICouponTypeService: ICrudService<CouponType>
    {
        Task<CouponType> UploadAsync(CouponType contract);

        Task<QueryResponse<CouponTypeListItem>> ListAsync(QueryRequest<CouponTypeListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

