using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class BfsSystemMapper
    {
        public static BfsSystem ToContract(this BfsSystemEntity entity)
        {
            var contract = new BfsSystem()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
BasePortNumber= entity.BasePortNumber,
DbPrefix= entity.DbPrefix,

               BfsClientId= entity.BfsClientId,
SystemTemplateId= entity.SystemTemplateId,

            };

            return contract;
        }

        public static List<BfsSystem> ToContract(this IEnumerable<BfsSystemEntity> BfsSystems)
        {
            return BfsSystems.Select(x => x.ToContract()).ToList();
        }

        public static List<BfsSystemEntity> ToEntity(this IEnumerable<BfsSystem> BfsSystems)
        {
            return BfsSystems.Select(x => x.ToEntity()).ToList();
        }

        public static BfsSystemEntity ToEntity(this BfsSystem contract, BfsSystemEntity entity = null)
        {
            var BfsSystemEntity = entity ?? new();

            BfsSystemEntity.IsDeleted= contract.IsDeleted;
BfsSystemEntity.Id= contract.Id;
BfsSystemEntity.Name= contract.Name;
BfsSystemEntity.Notes= contract.Notes;
BfsSystemEntity.BasePortNumber= contract.BasePortNumber;
BfsSystemEntity.DbPrefix= contract.DbPrefix;

            BfsSystemEntity.BfsClientId= contract.BfsClientId;
BfsSystemEntity.SystemTemplateId= contract.SystemTemplateId;

            return BfsSystemEntity;
        }     
    }
}
