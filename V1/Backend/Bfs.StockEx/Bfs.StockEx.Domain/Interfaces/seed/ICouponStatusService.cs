using Bfs.Core.Contracts;
using Bfs.Core.Interfaces;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface ICouponStatusService: ICrudService<CouponStatus>
    {
        Task<CouponStatus> UploadAsync(CouponStatus contract);

        Task<QueryResponse<CouponStatusListItem>> ListAsync(QueryRequest<CouponStatusListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}

