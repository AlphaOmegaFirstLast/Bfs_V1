using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class RoleMapper
    {
        public static Role ToContract(this RoleEntity entity)
        {
            var contract = new Role()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<Role> ToContract(this IEnumerable<RoleEntity> Roles)
        {
            return Roles.Select(x => x.ToContract()).ToList();
        }

        public static List<RoleEntity> ToEntity(this IEnumerable<Role> Roles)
        {
            return Roles.Select(x => x.ToEntity()).ToList();
        }

        public static RoleEntity ToEntity(this Role contract, RoleEntity entity = null)
        {
            var RoleEntity = entity ?? new();

            RoleEntity.IsDeleted= contract.IsDeleted;
RoleEntity.Id= contract.Id;
RoleEntity.Name= contract.Name;
RoleEntity.Notes= contract.Notes;

            return RoleEntity;
        }     
    }
}

