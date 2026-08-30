using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class ActionLocationMapper
    {
        public static ActionLocation ToContract(this ActionLocationEntity entity)
        {
            var contract = new ActionLocation()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<ActionLocation> ToContract(this IEnumerable<ActionLocationEntity> ActionLocations)
        {
            return ActionLocations.Select(x => x.ToContract()).ToList();
        }

        public static List<ActionLocationEntity> ToEntity(this IEnumerable<ActionLocation> ActionLocations)
        {
            return ActionLocations.Select(x => x.ToEntity()).ToList();
        }

        public static ActionLocationEntity ToEntity(this ActionLocation contract, ActionLocationEntity entity = null)
        {
            var ActionLocationEntity = entity ?? new();

            ActionLocationEntity.IsDeleted= contract.IsDeleted;
ActionLocationEntity.Id= contract.Id;
ActionLocationEntity.Name= contract.Name;
ActionLocationEntity.Notes= contract.Notes;

            return ActionLocationEntity;
        }     
    }
}
