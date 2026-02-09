using Bfs.Core.ObjectFields;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data.Models;

namespace Bfs.BestFit.Domain.Mapper
{
    public static class SystemInfoMapper
    {
        public static SystemInfo ToContract(this SystemInfoEntity entity)
        {
            var contract = new SystemInfo()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
BasePortNumber= entity.BasePortNumber,

               ClientId= entity.ClientId,
SystemTemplateId= entity.SystemTemplateId,

            };

            return contract;
        }

        public static List<SystemInfo> ToContract(this IEnumerable<SystemInfoEntity> SystemInfos)
        {
            return SystemInfos.Select(x => x.ToContract()).ToList();
        }

        public static List<SystemInfoEntity> ToEntity(this IEnumerable<SystemInfo> SystemInfos)
        {
            return SystemInfos.Select(x => x.ToEntity()).ToList();
        }

        public static SystemInfoEntity ToEntity(this SystemInfo contract, SystemInfoEntity entity = null)
        {
            var SystemInfoEntity = entity ?? new();

            SystemInfoEntity.IsDeleted= contract.IsDeleted;
SystemInfoEntity.Id= contract.Id;
SystemInfoEntity.Name= contract.Name;
SystemInfoEntity.Notes= contract.Notes;
SystemInfoEntity.BasePortNumber= contract.BasePortNumber;

            SystemInfoEntity.ClientId= contract.ClientId;
SystemInfoEntity.SystemTemplateId= contract.SystemTemplateId;

            return SystemInfoEntity;
        }     
    }
}
