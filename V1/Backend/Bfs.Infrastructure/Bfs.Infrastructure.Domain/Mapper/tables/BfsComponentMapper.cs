using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class BfsComponentMapper
    {
        public static BfsComponent ToContract(this BfsComponentEntity entity)
        {
            var contract = new BfsComponent()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
IsSoftDelete= entity.IsSoftDelete,
Name= entity.Name,
DisplayName= entity.DisplayName,
MenuName= entity.MenuName,
MenuPlaceHolder= entity.MenuPlaceHolder,
QueryBaseTable= entity.QueryBaseTable,
Notes= entity.Notes,

               BfsSystemId= entity.BfsSystemId,
DataTypeId= entity.DataTypeId,

            };

            return contract;
        }

        public static List<BfsComponent> ToContract(this IEnumerable<BfsComponentEntity> BfsComponents)
        {
            return BfsComponents.Select(x => x.ToContract()).ToList();
        }

        public static List<BfsComponentEntity> ToEntity(this IEnumerable<BfsComponent> BfsComponents)
        {
            return BfsComponents.Select(x => x.ToEntity()).ToList();
        }

        public static BfsComponentEntity ToEntity(this BfsComponent contract, BfsComponentEntity entity = null)
        {
            var BfsComponentEntity = entity ?? new();

            BfsComponentEntity.IsDeleted= contract.IsDeleted;
BfsComponentEntity.Id= contract.Id;
BfsComponentEntity.IsSoftDelete= contract.IsSoftDelete;
BfsComponentEntity.Name= contract.Name;
BfsComponentEntity.DisplayName= contract.DisplayName;
BfsComponentEntity.MenuName= contract.MenuName;
BfsComponentEntity.MenuPlaceHolder= contract.MenuPlaceHolder;
BfsComponentEntity.QueryBaseTable= contract.QueryBaseTable;
BfsComponentEntity.Notes= contract.Notes;

            BfsComponentEntity.BfsSystemId= contract.BfsSystemId;
BfsComponentEntity.DataTypeId= contract.DataTypeId;

            return BfsComponentEntity;
        }     
    }
}
