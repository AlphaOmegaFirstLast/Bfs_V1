using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class AuthRoleMapper
    {
        public static AuthRole ToContract(this AuthRoleEntity entity)
        {
            var contract = new AuthRole()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<AuthRole> ToContract(this IEnumerable<AuthRoleEntity> AuthRoles)
        {
            return AuthRoles.Select(x => x.ToContract()).ToList();
        }

        public static List<AuthRoleEntity> ToEntity(this IEnumerable<AuthRole> AuthRoles)
        {
            return AuthRoles.Select(x => x.ToEntity()).ToList();
        }

        public static AuthRoleEntity ToEntity(this AuthRole contract, AuthRoleEntity entity = null)
        {
            var AuthRoleEntity = entity ?? new();

            AuthRoleEntity.IsDeleted= contract.IsDeleted;
AuthRoleEntity.Id= contract.Id;
AuthRoleEntity.Name= contract.Name;
AuthRoleEntity.Notes= contract.Notes;

            return AuthRoleEntity;
        }     
    }
}
