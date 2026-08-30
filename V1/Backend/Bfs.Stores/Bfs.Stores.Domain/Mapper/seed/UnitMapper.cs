using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class UnitMapper
    {
        public static Unit ToContract(this UnitEntity entity)
        {
            var contract = new Unit()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<Unit> ToContract(this IEnumerable<UnitEntity> Units)
        {
            return Units.Select(x => x.ToContract()).ToList();
        }

        public static List<UnitEntity> ToEntity(this IEnumerable<Unit> Units)
        {
            return Units.Select(x => x.ToEntity()).ToList();
        }

        public static UnitEntity ToEntity(this Unit contract, UnitEntity entity = null)
        {
            var UnitEntity = entity ?? new();

            UnitEntity.IsDeleted= contract.IsDeleted;
UnitEntity.Id= contract.Id;
UnitEntity.Name= contract.Name;
UnitEntity.Notes= contract.Notes;

            return UnitEntity;
        }     
    }
}

