using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class AuthRoleUserMapper
    {
        public static AuthRoleUser ToContract(this AuthRoleUserEntity entity)
        {
            var contract = new AuthRoleUser()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               AuthRoleId= entity.AuthRoleId,
AuthUserId= entity.AuthUserId,

            };

            return contract;
        }

        public static List<AuthRoleUser> ToContract(this IEnumerable<AuthRoleUserEntity> AuthRoleUsers)
        {
            return AuthRoleUsers.Select(x => x.ToContract()).ToList();
        }

        public static List<AuthRoleUserEntity> ToEntity(this IEnumerable<AuthRoleUser> AuthRoleUsers)
        {
            return AuthRoleUsers.Select(x => x.ToEntity()).ToList();
        }

        public static AuthRoleUserEntity ToEntity(this AuthRoleUser contract, AuthRoleUserEntity entity = null)
        {
            var AuthRoleUserEntity = entity ?? new();

            AuthRoleUserEntity.IsDeleted= contract.IsDeleted;
AuthRoleUserEntity.Id= contract.Id;

            AuthRoleUserEntity.AuthRoleId= contract.AuthRoleId;
AuthRoleUserEntity.AuthUserId= contract.AuthUserId;

            return AuthRoleUserEntity;
        }     
    }
}
