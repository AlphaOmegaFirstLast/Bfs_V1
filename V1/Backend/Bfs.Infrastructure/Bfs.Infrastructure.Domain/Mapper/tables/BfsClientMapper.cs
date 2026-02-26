using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class BfsClientMapper
    {
        public static BfsClient ToContract(this BfsClientEntity entity)
        {
            var contract = new BfsClient()
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

        public static List<BfsClient> ToContract(this IEnumerable<BfsClientEntity> BfsClients)
        {
            return BfsClients.Select(x => x.ToContract()).ToList();
        }

        public static List<BfsClientEntity> ToEntity(this IEnumerable<BfsClient> BfsClients)
        {
            return BfsClients.Select(x => x.ToEntity()).ToList();
        }

        public static BfsClientEntity ToEntity(this BfsClient contract, BfsClientEntity entity = null)
        {
            var BfsClientEntity = entity ?? new();

            BfsClientEntity.DbConnection= contract.DbConnection;
BfsClientEntity.IsDeleted= contract.IsDeleted;
BfsClientEntity.Id= contract.Id;
BfsClientEntity.Name= contract.Name;
BfsClientEntity.Notes= contract.Notes;

            BfsClientEntity.CustomFields= contract.CustomFields.ToEntity();

            return BfsClientEntity;
        }     
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

