using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class StoreMapper
    {
        public static Store ToContract(this StoreEntity entity)
        {
            var contract = new Store()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<Store> ToContract(this IEnumerable<StoreEntity> Stores)
        {
            return Stores.Select(x => x.ToContract()).ToList();
        }

        public static List<StoreEntity> ToEntity(this IEnumerable<Store> Stores)
        {
            return Stores.Select(x => x.ToEntity()).ToList();
        }

        public static StoreEntity ToEntity(this Store contract, StoreEntity entity = null)
        {
            var StoreEntity = entity ?? new();

            StoreEntity.IsDeleted= contract.IsDeleted;
StoreEntity.Id= contract.Id;
StoreEntity.Name= contract.Name;
StoreEntity.Notes= contract.Notes;

            return StoreEntity;
        }     
    }
}

