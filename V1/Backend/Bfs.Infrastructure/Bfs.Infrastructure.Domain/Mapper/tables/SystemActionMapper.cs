using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class SystemActionMapper
    {
        public static SystemAction ToContract(this SystemActionEntity entity)
        {
            var contract = new SystemAction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

               ActionTypeId= entity.ActionTypeId,

            };

            return contract;
        }

        public static List<SystemAction> ToContract(this IEnumerable<SystemActionEntity> SystemActions)
        {
            return SystemActions.Select(x => x.ToContract()).ToList();
        }

        public static List<SystemActionEntity> ToEntity(this IEnumerable<SystemAction> SystemActions)
        {
            return SystemActions.Select(x => x.ToEntity()).ToList();
        }

        public static SystemActionEntity ToEntity(this SystemAction contract, SystemActionEntity entity = null)
        {
            var SystemActionEntity = entity ?? new();

            SystemActionEntity.IsDeleted= contract.IsDeleted;
SystemActionEntity.Id= contract.Id;
SystemActionEntity.Name= contract.Name;
SystemActionEntity.Notes= contract.Notes;

            SystemActionEntity.ActionTypeId= contract.ActionTypeId;

            return SystemActionEntity;
        }     
    }
}
