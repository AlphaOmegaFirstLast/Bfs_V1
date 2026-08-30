using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class AreaMapper
    {
        public static Area ToContract(this AreaEntity entity)
        {
            var contract = new Area()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<Area> ToContract(this IEnumerable<AreaEntity> Areas)
        {
            return Areas.Select(x => x.ToContract()).ToList();
        }

        public static List<AreaEntity> ToEntity(this IEnumerable<Area> Areas)
        {
            return Areas.Select(x => x.ToEntity()).ToList();
        }

        public static AreaEntity ToEntity(this Area contract, AreaEntity entity = null)
        {
            var AreaEntity = entity ?? new();

            AreaEntity.IsDeleted= contract.IsDeleted;
AreaEntity.Id= contract.Id;
AreaEntity.Name= contract.Name;
AreaEntity.Notes= contract.Notes;

            return AreaEntity;
        }     
    }
}
