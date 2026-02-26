using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class BfsClientSystemMapper
    {
        public static BfsClientSystem ToContract(this BfsClientSystemEntity entity)
        {
            var contract = new BfsClientSystem()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               BfsClientId= entity.BfsClientId,
BfsSystemId= entity.BfsSystemId,

            };

            return contract;
        }

        public static List<BfsClientSystem> ToContract(this IEnumerable<BfsClientSystemEntity> BfsClientSystems)
        {
            return BfsClientSystems.Select(x => x.ToContract()).ToList();
        }

        public static List<BfsClientSystemEntity> ToEntity(this IEnumerable<BfsClientSystem> BfsClientSystems)
        {
            return BfsClientSystems.Select(x => x.ToEntity()).ToList();
        }

        public static BfsClientSystemEntity ToEntity(this BfsClientSystem contract, BfsClientSystemEntity entity = null)
        {
            var BfsClientSystemEntity = entity ?? new();

            BfsClientSystemEntity.IsDeleted= contract.IsDeleted;
BfsClientSystemEntity.Id= contract.Id;

            BfsClientSystemEntity.BfsClientId= contract.BfsClientId;
BfsClientSystemEntity.BfsSystemId= contract.BfsSystemId;

            return BfsClientSystemEntity;
        }     
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

