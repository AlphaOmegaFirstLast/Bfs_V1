using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class BfsComponentSystemActionMapper
    {
        public static BfsComponentSystemAction ToContract(this BfsComponentSystemActionEntity entity)
        {
            var contract = new BfsComponentSystemAction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               BfsComponentId= entity.BfsComponentId,
SystemActionId= entity.SystemActionId,
ActionLocationId= entity.ActionLocationId,

            };

            return contract;
        }

        public static List<BfsComponentSystemAction> ToContract(this IEnumerable<BfsComponentSystemActionEntity> BfsComponentSystemActions)
        {
            return BfsComponentSystemActions.Select(x => x.ToContract()).ToList();
        }

        public static List<BfsComponentSystemActionEntity> ToEntity(this IEnumerable<BfsComponentSystemAction> BfsComponentSystemActions)
        {
            return BfsComponentSystemActions.Select(x => x.ToEntity()).ToList();
        }

        public static BfsComponentSystemActionEntity ToEntity(this BfsComponentSystemAction contract, BfsComponentSystemActionEntity entity = null)
        {
            var BfsComponentSystemActionEntity = entity ?? new();

            BfsComponentSystemActionEntity.IsDeleted= contract.IsDeleted;
BfsComponentSystemActionEntity.Id= contract.Id;

            BfsComponentSystemActionEntity.BfsComponentId= contract.BfsComponentId;
BfsComponentSystemActionEntity.SystemActionId= contract.SystemActionId;
BfsComponentSystemActionEntity.ActionLocationId= contract.ActionLocationId;

            return BfsComponentSystemActionEntity;
        }     
    }
}
