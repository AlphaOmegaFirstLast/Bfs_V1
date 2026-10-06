using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class PaymentModeMapper
    {
        public static PaymentMode ToContract(this PaymentModeEntity entity)
        {
            var contract = new PaymentMode()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<PaymentMode> ToContract(this IEnumerable<PaymentModeEntity> PaymentModes)
        {
            return PaymentModes.Select(x => x.ToContract()).ToList();
        }

        public static List<PaymentModeEntity> ToEntity(this IEnumerable<PaymentMode> PaymentModes)
        {
            return PaymentModes.Select(x => x.ToEntity()).ToList();
        }

        public static PaymentModeEntity ToEntity(this PaymentMode contract, PaymentModeEntity entity = null)
        {
            var PaymentModeEntity = entity ?? new();

            PaymentModeEntity.IsDeleted= contract.IsDeleted;
PaymentModeEntity.Id= contract.Id;
PaymentModeEntity.Name= contract.Name;
PaymentModeEntity.Notes= contract.Notes;

            return PaymentModeEntity;
        }     
    }
}

