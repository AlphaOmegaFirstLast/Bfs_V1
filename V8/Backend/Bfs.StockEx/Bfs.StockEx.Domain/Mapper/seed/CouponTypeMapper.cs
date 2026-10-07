using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class CouponTypeMapper
    {
        public static CouponType ToContract(this CouponTypeEntity entity)
        {
            var contract = new CouponType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<CouponType> ToContract(this IEnumerable<CouponTypeEntity> CouponTypes)
        {
            return CouponTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<CouponTypeEntity> ToEntity(this IEnumerable<CouponType> CouponTypes)
        {
            return CouponTypes.Select(x => x.ToEntity()).ToList();
        }

        public static CouponTypeEntity ToEntity(this CouponType contract, CouponTypeEntity entity = null)
        {
            var CouponTypeEntity = entity ?? new();

            CouponTypeEntity.IsDeleted= contract.IsDeleted;
CouponTypeEntity.Id= contract.Id;
CouponTypeEntity.Name= contract.Name;
CouponTypeEntity.Notes= contract.Notes;

            return CouponTypeEntity;
        }     
    }
}

