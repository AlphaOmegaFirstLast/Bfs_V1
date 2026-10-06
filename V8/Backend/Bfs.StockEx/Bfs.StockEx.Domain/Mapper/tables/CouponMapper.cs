using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class CouponMapper
    {
        public static Coupon ToContract(this CouponEntity entity)
        {
            var contract = new Coupon()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
Value= entity.Value,
AnnounceDate= entity.AnnounceDate,
ValueDate= entity.ValueDate,
DueDate= entity.DueDate,
CouponPercent= entity.CouponPercent,

               TradingRoomId= entity.TradingRoomId,
StockShareId= entity.StockShareId,
CouponTypeId= entity.CouponTypeId,
CouponStatusId= entity.CouponStatusId,

            };

            return contract;
        }

        public static List<Coupon> ToContract(this IEnumerable<CouponEntity> Coupons)
        {
            return Coupons.Select(x => x.ToContract()).ToList();
        }

        public static List<CouponEntity> ToEntity(this IEnumerable<Coupon> Coupons)
        {
            return Coupons.Select(x => x.ToEntity()).ToList();
        }

        public static CouponEntity ToEntity(this Coupon contract, CouponEntity entity = null)
        {
            var CouponEntity = entity ?? new();

            CouponEntity.IsDeleted= contract.IsDeleted;
CouponEntity.Id= contract.Id;
CouponEntity.Name= contract.Name;
CouponEntity.Notes= contract.Notes;
CouponEntity.Value= contract.Value;
CouponEntity.AnnounceDate= contract.AnnounceDate;
CouponEntity.ValueDate= contract.ValueDate;
CouponEntity.DueDate= contract.DueDate;
CouponEntity.CouponPercent= contract.CouponPercent;

            CouponEntity.TradingRoomId= contract.TradingRoomId;
CouponEntity.StockShareId= contract.StockShareId;
CouponEntity.CouponTypeId= contract.CouponTypeId;
CouponEntity.CouponStatusId= contract.CouponStatusId;

            return CouponEntity;
        }     
    }
}

