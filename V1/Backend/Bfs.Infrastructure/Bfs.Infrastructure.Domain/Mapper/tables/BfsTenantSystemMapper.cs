using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class BfsTenantSystemMapper
    {
        public static BfsTenantSystem ToContract(this BfsTenantSystemEntity entity)
        {
            var contract = new BfsTenantSystem()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               BfsTenantId= entity.BfsTenantId,
BfsSystemId= entity.BfsSystemId,

            };

            return contract;
        }

        public static List<BfsTenantSystem> ToContract(this IEnumerable<BfsTenantSystemEntity> BfsTenantSystems)
        {
            return BfsTenantSystems.Select(x => x.ToContract()).ToList();
        }

        public static List<BfsTenantSystemEntity> ToEntity(this IEnumerable<BfsTenantSystem> BfsTenantSystems)
        {
            return BfsTenantSystems.Select(x => x.ToEntity()).ToList();
        }

        public static BfsTenantSystemEntity ToEntity(this BfsTenantSystem contract, BfsTenantSystemEntity entity = null)
        {
            var BfsTenantSystemEntity = entity ?? new();

            BfsTenantSystemEntity.IsDeleted= contract.IsDeleted;
BfsTenantSystemEntity.Id= contract.Id;

            BfsTenantSystemEntity.BfsTenantId= contract.BfsTenantId;
BfsTenantSystemEntity.BfsSystemId= contract.BfsSystemId;

            return BfsTenantSystemEntity;
        }     
    }
}
