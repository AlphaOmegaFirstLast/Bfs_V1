using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class StrProductMapper
    {
        public static StrProduct ToContract(this StrProductEntity entity)
        {
            var contract = new StrProduct()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<StrProduct> ToContract(this IEnumerable<StrProductEntity> StrProducts)
        {
            return StrProducts.Select(x => x.ToContract()).ToList();
        }

        public static List<StrProductEntity> ToEntity(this IEnumerable<StrProduct> StrProducts)
        {
            return StrProducts.Select(x => x.ToEntity()).ToList();
        }

        public static StrProductEntity ToEntity(this StrProduct contract, StrProductEntity entity = null)
        {
            var StrProductEntity = entity ?? new();

            StrProductEntity.IsDeleted= contract.IsDeleted;
StrProductEntity.Id= contract.Id;
StrProductEntity.Name= contract.Name;
StrProductEntity.Notes= contract.Notes;

            return StrProductEntity;
        }     
    }
}
