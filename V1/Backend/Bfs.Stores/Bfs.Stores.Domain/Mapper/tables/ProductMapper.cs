using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class ProductMapper
    {
        public static Product ToContract(this ProductEntity entity)
        {
            var contract = new Product()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<Product> ToContract(this IEnumerable<ProductEntity> Products)
        {
            return Products.Select(x => x.ToContract()).ToList();
        }

        public static List<ProductEntity> ToEntity(this IEnumerable<Product> Products)
        {
            return Products.Select(x => x.ToEntity()).ToList();
        }

        public static ProductEntity ToEntity(this Product contract, ProductEntity entity = null)
        {
            var ProductEntity = entity ?? new();

            ProductEntity.IsDeleted= contract.IsDeleted;
ProductEntity.Id= contract.Id;
ProductEntity.Name= contract.Name;
ProductEntity.Notes= contract.Notes;

            return ProductEntity;
        }     
    }
}

