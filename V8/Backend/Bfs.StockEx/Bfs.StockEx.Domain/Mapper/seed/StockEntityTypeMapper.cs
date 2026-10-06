using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class StockEntityTypeMapper
    {
        public static StockEntityType ToContract(this StockEntityTypeEntity entity)
        {
            var contract = new StockEntityType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<StockEntityType> ToContract(this IEnumerable<StockEntityTypeEntity> StockEntityTypes)
        {
            return StockEntityTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<StockEntityTypeEntity> ToEntity(this IEnumerable<StockEntityType> StockEntityTypes)
        {
            return StockEntityTypes.Select(x => x.ToEntity()).ToList();
        }

        public static StockEntityTypeEntity ToEntity(this StockEntityType contract, StockEntityTypeEntity entity = null)
        {
            var StockEntityTypeEntity = entity ?? new();

            StockEntityTypeEntity.IsDeleted= contract.IsDeleted;
StockEntityTypeEntity.Id= contract.Id;
StockEntityTypeEntity.Name= contract.Name;
StockEntityTypeEntity.Notes= contract.Notes;

            return StockEntityTypeEntity;
        }     
    }
}

