using Bfs.Core.ObjectFields;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data.Models;

namespace Bfs.BestFit.Domain.Mapper
{
    public static class ComponentBusinessActionMapper
    {
        public static ComponentBusinessAction ToContract(this ComponentBusinessActionEntity entity)
        {
            var contract = new ComponentBusinessAction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               ComponentId= entity.ComponentId,
BusinessActionId= entity.BusinessActionId,
ActionLocationId= entity.ActionLocationId,

            };

            return contract;
        }

        public static List<ComponentBusinessAction> ToContract(this IEnumerable<ComponentBusinessActionEntity> ComponentBusinessActions)
        {
            return ComponentBusinessActions.Select(x => x.ToContract()).ToList();
        }

        public static List<ComponentBusinessActionEntity> ToEntity(this IEnumerable<ComponentBusinessAction> ComponentBusinessActions)
        {
            return ComponentBusinessActions.Select(x => x.ToEntity()).ToList();
        }

        public static ComponentBusinessActionEntity ToEntity(this ComponentBusinessAction contract, ComponentBusinessActionEntity entity = null)
        {
            var ComponentBusinessActionEntity = entity ?? new();

            ComponentBusinessActionEntity.IsDeleted= contract.IsDeleted;
ComponentBusinessActionEntity.Id= contract.Id;

            ComponentBusinessActionEntity.ComponentId= contract.ComponentId;
ComponentBusinessActionEntity.BusinessActionId= contract.BusinessActionId;
ComponentBusinessActionEntity.ActionLocationId= contract.ActionLocationId;

            return ComponentBusinessActionEntity;
        }     
    }
}
