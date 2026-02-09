using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class BfsComponentBusinessActionMapper
    {
        public static BfsComponentBusinessAction ToContract(this BfsComponentBusinessActionEntity entity)
        {
            var contract = new BfsComponentBusinessAction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               BfsComponentId= entity.BfsComponentId,
BusinessActionId= entity.BusinessActionId,
ActionLocationId= entity.ActionLocationId,

            };

            return contract;
        }

        public static List<BfsComponentBusinessAction> ToContract(this IEnumerable<BfsComponentBusinessActionEntity> BfsComponentBusinessActions)
        {
            return BfsComponentBusinessActions.Select(x => x.ToContract()).ToList();
        }

        public static List<BfsComponentBusinessActionEntity> ToEntity(this IEnumerable<BfsComponentBusinessAction> BfsComponentBusinessActions)
        {
            return BfsComponentBusinessActions.Select(x => x.ToEntity()).ToList();
        }

        public static BfsComponentBusinessActionEntity ToEntity(this BfsComponentBusinessAction contract, BfsComponentBusinessActionEntity entity = null)
        {
            var BfsComponentBusinessActionEntity = entity ?? new();

            BfsComponentBusinessActionEntity.IsDeleted= contract.IsDeleted;
BfsComponentBusinessActionEntity.Id= contract.Id;

            BfsComponentBusinessActionEntity.BfsComponentId= contract.BfsComponentId;
BfsComponentBusinessActionEntity.BusinessActionId= contract.BusinessActionId;
BfsComponentBusinessActionEntity.ActionLocationId= contract.ActionLocationId;

            return BfsComponentBusinessActionEntity;
        }     
    }
}
