using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class RoleAppMapper
    {
        public static RoleApp ToContract(this RoleAppEntity entity)
        {
            var contract = new RoleApp()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               RoleId= entity.RoleId,
AppId= entity.AppId,

            };

            return contract;
        }

        public static List<RoleApp> ToContract(this IEnumerable<RoleAppEntity> RoleApps)
        {
            return RoleApps.Select(x => x.ToContract()).ToList();
        }

        public static List<RoleAppEntity> ToEntity(this IEnumerable<RoleApp> RoleApps)
        {
            return RoleApps.Select(x => x.ToEntity()).ToList();
        }

        public static RoleAppEntity ToEntity(this RoleApp contract, RoleAppEntity entity = null)
        {
            var RoleAppEntity = entity ?? new();

            RoleAppEntity.IsDeleted= contract.IsDeleted;
RoleAppEntity.Id= contract.Id;

            RoleAppEntity.RoleId= contract.RoleId;
RoleAppEntity.AppId= contract.AppId;

            return RoleAppEntity;
        }     
    }
}

