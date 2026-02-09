using Bfs.Core.ObjectFields;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data.Models;

namespace Bfs.BestFit.Domain.Mapper
{
    public static class ComponentSystemActionMapper
    {
        public static ComponentSystemAction ToContract(this ComponentSystemActionEntity entity)
        {
            var contract = new ComponentSystemAction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               ComponentId= entity.ComponentId,
SystemActionId= entity.SystemActionId,
ActionLocationId= entity.ActionLocationId,

            };

            return contract;
        }

        public static List<ComponentSystemAction> ToContract(this IEnumerable<ComponentSystemActionEntity> ComponentSystemActions)
        {
            return ComponentSystemActions.Select(x => x.ToContract()).ToList();
        }

        public static List<ComponentSystemActionEntity> ToEntity(this IEnumerable<ComponentSystemAction> ComponentSystemActions)
        {
            return ComponentSystemActions.Select(x => x.ToEntity()).ToList();
        }

        public static ComponentSystemActionEntity ToEntity(this ComponentSystemAction contract, ComponentSystemActionEntity entity = null)
        {
            var ComponentSystemActionEntity = entity ?? new();

            ComponentSystemActionEntity.IsDeleted= contract.IsDeleted;
ComponentSystemActionEntity.Id= contract.Id;

            ComponentSystemActionEntity.ComponentId= contract.ComponentId;
ComponentSystemActionEntity.SystemActionId= contract.SystemActionId;
ComponentSystemActionEntity.ActionLocationId= contract.ActionLocationId;

            return ComponentSystemActionEntity;
        }     
    }
}
