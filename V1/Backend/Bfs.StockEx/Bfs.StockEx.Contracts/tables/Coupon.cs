using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class Coupon : IIdentifiable 
    {
        ///<Summary>
        /// Coupon IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Coupon ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Coupon Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Coupon Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// Coupon Value.
        ///</Summary>
        public decimal Value {get; set;} = 0 ;
///<Summary>
        /// Coupon Announce Date.
        ///</Summary>
        public DateTime AnnounceDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// Coupon Value Date.
        ///</Summary>
        public DateTime ValueDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// Coupon Due Date.
        ///</Summary>
        public DateTime DueDate {get; set;} = DateTime.MinValue ;
///<Summary>
        /// Coupon Percent.
        ///</Summary>
        public decimal CouponPercent {get; set;} = 0 ;

        ///<Summary>
        /// Coupon Trading Room.
        ///</Summary>
        public long TradingRoomId {get; set;} = 0 ;
///<Summary>
        /// Coupon Stock Share.
        ///</Summary>
        public long StockShareId {get; set;} = 0 ;
///<Summary>
        /// Coupon Coupon Type.
        ///</Summary>
        public long CouponTypeId {get; set;} = 0 ;
///<Summary>
        /// Coupon Coupon Status.
        ///</Summary>
        public long CouponStatusId {get; set;} = 0 ;

    }
}

