using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class AuthRoleAppMapper
    {
        public static AuthRoleApp ToContract(this AuthRoleAppEntity entity)
        {
            var contract = new AuthRoleApp()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               AuthRoleId= entity.AuthRoleId,
AuthAppId= entity.AuthAppId,

            };

            return contract;
        }

        public static List<AuthRoleApp> ToContract(this IEnumerable<AuthRoleAppEntity> AuthRoleApps)
        {
            return AuthRoleApps.Select(x => x.ToContract()).ToList();
        }

        public static List<AuthRoleAppEntity> ToEntity(this IEnumerable<AuthRoleApp> AuthRoleApps)
        {
            return AuthRoleApps.Select(x => x.ToEntity()).ToList();
        }

        public static AuthRoleAppEntity ToEntity(this AuthRoleApp contract, AuthRoleAppEntity entity = null)
        {
            var AuthRoleAppEntity = entity ?? new();

            AuthRoleAppEntity.IsDeleted= contract.IsDeleted;
AuthRoleAppEntity.Id= contract.Id;

            AuthRoleAppEntity.AuthRoleId= contract.AuthRoleId;
AuthRoleAppEntity.AuthAppId= contract.AuthAppId;

            return AuthRoleAppEntity;
        }     
    }
}
