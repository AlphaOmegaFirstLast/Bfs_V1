using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class CouponStatusMapper
    {
        public static CouponStatus ToContract(this CouponStatusEntity entity)
        {
            var contract = new CouponStatus()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<CouponStatus> ToContract(this IEnumerable<CouponStatusEntity> CouponStatuss)
        {
            return CouponStatuss.Select(x => x.ToContract()).ToList();
        }

        public static List<CouponStatusEntity> ToEntity(this IEnumerable<CouponStatus> CouponStatuss)
        {
            return CouponStatuss.Select(x => x.ToEntity()).ToList();
        }

        public static CouponStatusEntity ToEntity(this CouponStatus contract, CouponStatusEntity entity = null)
        {
            var CouponStatusEntity = entity ?? new();

            CouponStatusEntity.IsDeleted= contract.IsDeleted;
CouponStatusEntity.Id= contract.Id;
CouponStatusEntity.Name= contract.Name;
CouponStatusEntity.Notes= contract.Notes;

            return CouponStatusEntity;
        }     
    }
}

