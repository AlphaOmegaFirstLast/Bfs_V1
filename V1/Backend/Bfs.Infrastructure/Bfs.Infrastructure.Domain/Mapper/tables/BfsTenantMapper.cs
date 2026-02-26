using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class BfsTenantMapper
    {
        public static BfsTenant ToContract(this BfsTenantEntity entity)
        {
            var contract = new BfsTenant()
            {
               DbConnection= entity.DbConnection,
IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

               CustomFields= entity.CustomFields.ToContract(),

            };

            return contract;
        }

        public static List<BfsTenant> ToContract(this IEnumerable<BfsTenantEntity> BfsTenants)
        {
            return BfsTenants.Select(x => x.ToContract()).ToList();
        }

        public static List<BfsTenantEntity> ToEntity(this IEnumerable<BfsTenant> BfsTenants)
        {
            return BfsTenants.Select(x => x.ToEntity()).ToList();
        }

        public static BfsTenantEntity ToEntity(this BfsTenant contract, BfsTenantEntity entity = null)
        {
            var BfsTenantEntity = entity ?? new();

            BfsTenantEntity.DbConnection= contract.DbConnection;
BfsTenantEntity.IsDeleted= contract.IsDeleted;
BfsTenantEntity.Id= contract.Id;
BfsTenantEntity.Name= contract.Name;
BfsTenantEntity.Notes= contract.Notes;

            BfsTenantEntity.CustomFields= contract.CustomFields.ToEntity();

            return BfsTenantEntity;
        }     
    }
}
