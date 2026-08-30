using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class TransferCostTypeMapper
    {
        public static TransferCostType ToContract(this TransferCostTypeEntity entity)
        {
            var contract = new TransferCostType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<TransferCostType> ToContract(this IEnumerable<TransferCostTypeEntity> TransferCostTypes)
        {
            return TransferCostTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<TransferCostTypeEntity> ToEntity(this IEnumerable<TransferCostType> TransferCostTypes)
        {
            return TransferCostTypes.Select(x => x.ToEntity()).ToList();
        }

        public static TransferCostTypeEntity ToEntity(this TransferCostType contract, TransferCostTypeEntity entity = null)
        {
            var TransferCostTypeEntity = entity ?? new();

            TransferCostTypeEntity.IsDeleted= contract.IsDeleted;
TransferCostTypeEntity.Id= contract.Id;
TransferCostTypeEntity.Name= contract.Name;
TransferCostTypeEntity.Notes= contract.Notes;

            return TransferCostTypeEntity;
        }     
    }
}

