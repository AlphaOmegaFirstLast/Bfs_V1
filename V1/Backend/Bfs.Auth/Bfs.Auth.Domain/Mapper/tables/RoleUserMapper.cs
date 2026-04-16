using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class RoleUserMapper
    {
        public static RoleUser ToContract(this RoleUserEntity entity)
        {
            var contract = new RoleUser()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               RoleId= entity.RoleId,

            };

            return contract;
        }

        public static List<RoleUser> ToContract(this IEnumerable<RoleUserEntity> RoleUsers)
        {
            return RoleUsers.Select(x => x.ToContract()).ToList();
        }

        public static List<RoleUserEntity> ToEntity(this IEnumerable<RoleUser> RoleUsers)
        {
            return RoleUsers.Select(x => x.ToEntity()).ToList();
        }

        public static RoleUserEntity ToEntity(this RoleUser contract, RoleUserEntity entity = null)
        {
            var RoleUserEntity = entity ?? new();

            RoleUserEntity.IsDeleted= contract.IsDeleted;
RoleUserEntity.Id= contract.Id;

            RoleUserEntity.RoleId= contract.RoleId;

            return RoleUserEntity;
        }     
    }
}

