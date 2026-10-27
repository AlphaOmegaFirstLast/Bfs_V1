using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CouponProcess : IIdentifiable 
    {
        ///<Summary>
        /// CouponProcess IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// CouponProcess ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// CouponProcess Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// CouponProcess Process Date.
        ///</Summary>
        public DateTime ProcessDate {get; set;} = DateTime.MinValue ;

        ///<Summary>
        /// CouponProcess Coupon.
        ///</Summary>
        public long CouponId {get; set;} = 0 ;
///<Summary>
        /// CouponProcess Process Status.
        ///</Summary>
        public long ProcessStatusId {get; set;} = 0 ;

    }
}