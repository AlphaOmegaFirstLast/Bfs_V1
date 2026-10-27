using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class CouponProcessMapper
    {
        public static CouponProcess ToContract(this CouponProcessEntity entity)
        {
            var contract = new CouponProcess()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Notes= entity.Notes,
ProcessDate= entity.ProcessDate,

               CouponId= entity.CouponId,
ProcessStatusId= entity.ProcessStatusId,

            };

            return contract;
        }

        public static List<CouponProcess> ToContract(this IEnumerable<CouponProcessEntity> CouponProcesss)
        {
            return CouponProcesss.Select(x => x.ToContract()).ToList();
        }

        public static List<CouponProcessEntity> ToEntity(this IEnumerable<CouponProcess> CouponProcesss)
        {
            return CouponProcesss.Select(x => x.ToEntity()).ToList();
        }

        public static CouponProcessEntity ToEntity(this CouponProcess contract, CouponProcessEntity entity = null)
        {
            var CouponProcessEntity = entity ?? new();

            CouponProcessEntity.IsDeleted= contract.IsDeleted;
CouponProcessEntity.Id= contract.Id;
CouponProcessEntity.Notes= contract.Notes;
CouponProcessEntity.ProcessDate= contract.ProcessDate;

            CouponProcessEntity.CouponId= contract.CouponId;
CouponProcessEntity.ProcessStatusId= contract.ProcessStatusId;

            return CouponProcessEntity;
        }     
    }
}

