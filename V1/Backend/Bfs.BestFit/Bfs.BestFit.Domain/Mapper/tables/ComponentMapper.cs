using Bfs.Core.ObjectFields;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data.Models;

namespace Bfs.BestFit.Domain.Mapper
{
    public static class ComponentMapper
    {
        public static Component ToContract(this ComponentEntity entity)
        {
            var contract = new Component()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
IsSoftDelete= entity.IsSoftDelete,
Name= entity.Name,
DisplayName= entity.DisplayName,
MenuName= entity.MenuName,
MenuPlaceHolder= entity.MenuPlaceHolder,
Notes= entity.Notes,
QueryBaseTable= entity.QueryBaseTable,

               SystemInfoId= entity.SystemInfoId,
DataTypeId= entity.DataTypeId,

            };

            return contract;
        }

        public static List<Component> ToContract(this IEnumerable<ComponentEntity> Components)
        {
            return Components.Select(x => x.ToContract()).ToList();
        }

        public static List<ComponentEntity> ToEntity(this IEnumerable<Component> Components)
        {
            return Components.Select(x => x.ToEntity()).ToList();
        }

        public static ComponentEntity ToEntity(this Component contract, ComponentEntity entity = null)
        {
            var ComponentEntity = entity ?? new();

            ComponentEntity.IsDeleted= contract.IsDeleted;
ComponentEntity.Id= contract.Id;
ComponentEntity.IsSoftDelete= contract.IsSoftDelete;
ComponentEntity.Name= contract.Name;
ComponentEntity.DisplayName= contract.DisplayName;
ComponentEntity.MenuName= contract.MenuName;
ComponentEntity.MenuPlaceHolder= contract.MenuPlaceHolder;
ComponentEntity.Notes= contract.Notes;
ComponentEntity.QueryBaseTable= contract.QueryBaseTable;

            ComponentEntity.SystemInfoId= contract.SystemInfoId;
ComponentEntity.DataTypeId= contract.DataTypeId;

            return ComponentEntity;
        }     
    }
}
