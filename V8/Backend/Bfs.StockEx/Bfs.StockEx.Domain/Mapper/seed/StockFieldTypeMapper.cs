using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class StockFieldTypeMapper
    {
        public static StockFieldType ToContract(this StockFieldTypeEntity entity)
        {
            var contract = new StockFieldType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<StockFieldType> ToContract(this IEnumerable<StockFieldTypeEntity> StockFieldTypes)
        {
            return StockFieldTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<StockFieldTypeEntity> ToEntity(this IEnumerable<StockFieldType> StockFieldTypes)
        {
            return StockFieldTypes.Select(x => x.ToEntity()).ToList();
        }

        public static StockFieldTypeEntity ToEntity(this StockFieldType contract, StockFieldTypeEntity entity = null)
        {
            var StockFieldTypeEntity = entity ?? new();

            StockFieldTypeEntity.IsDeleted= contract.IsDeleted;
StockFieldTypeEntity.Id= contract.Id;
StockFieldTypeEntity.Name= contract.Name;
StockFieldTypeEntity.Notes= contract.Notes;

            return StockFieldTypeEntity;
        }     
    }
}

