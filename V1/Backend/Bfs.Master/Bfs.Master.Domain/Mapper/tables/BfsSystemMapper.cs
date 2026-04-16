using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class BfsSystemMapper
    {
        public static BfsSystem ToContract(this BfsSystemEntity entity)
        {
            var contract = new BfsSystem()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
IsMaster= entity.IsMaster,
Notes= entity.Notes,
BasePortNumber= entity.BasePortNumber,
DbPrefix= entity.DbPrefix,
Logo= entity.Logo,
Name= entity.Name,

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
BfsSystemEntity.IsMaster= contract.IsMaster;
BfsSystemEntity.Notes= contract.Notes;
BfsSystemEntity.BasePortNumber= contract.BasePortNumber;
BfsSystemEntity.DbPrefix= contract.DbPrefix;
BfsSystemEntity.Logo= contract.Logo;
BfsSystemEntity.Name= contract.Name;

            BfsSystemEntity.SystemTemplateId= contract.SystemTemplateId;

            return BfsSystemEntity;
        }     
    }
}
