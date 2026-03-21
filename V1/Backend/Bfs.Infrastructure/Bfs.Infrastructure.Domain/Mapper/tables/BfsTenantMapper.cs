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
Logo= entity.Logo,
Notes= entity.Notes,
Name= entity.Name,
CompanyName= entity.CompanyName,

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
BfsTenantEntity.Logo= contract.Logo;
BfsTenantEntity.Notes= contract.Notes;
BfsTenantEntity.Name= contract.Name;
BfsTenantEntity.CompanyName= contract.CompanyName;

            BfsTenantEntity.CustomFields= contract.CustomFields.ToEntity();

            return BfsTenantEntity;
        }     
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

