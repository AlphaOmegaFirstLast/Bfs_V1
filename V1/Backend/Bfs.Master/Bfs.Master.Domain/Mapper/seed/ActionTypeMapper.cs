using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class ActionTypeMapper
    {
        public static ActionType ToContract(this ActionTypeEntity entity)
        {
            var contract = new ActionType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<ActionType> ToContract(this IEnumerable<ActionTypeEntity> ActionTypes)
        {
            return ActionTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<ActionTypeEntity> ToEntity(this IEnumerable<ActionType> ActionTypes)
        {
            return ActionTypes.Select(x => x.ToEntity()).ToList();
        }

        public static ActionTypeEntity ToEntity(this ActionType contract, ActionTypeEntity entity = null)
        {
            var ActionTypeEntity = entity ?? new();

            ActionTypeEntity.IsDeleted= contract.IsDeleted;
ActionTypeEntity.Id= contract.Id;
ActionTypeEntity.Name= contract.Name;
ActionTypeEntity.Notes= contract.Notes;

            return ActionTypeEntity;
        }     
    }
}
