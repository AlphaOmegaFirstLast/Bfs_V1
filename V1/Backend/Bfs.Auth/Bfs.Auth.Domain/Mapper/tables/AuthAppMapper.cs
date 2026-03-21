using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class AuthAppMapper
    {
        public static AuthApp ToContract(this AuthAppEntity entity)
        {
            var contract = new AuthApp()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

               BfsSystemId= entity.BfsSystemId,

            };

            return contract;
        }

        public static List<AuthApp> ToContract(this IEnumerable<AuthAppEntity> AuthApps)
        {
            return AuthApps.Select(x => x.ToContract()).ToList();
        }

        public static List<AuthAppEntity> ToEntity(this IEnumerable<AuthApp> AuthApps)
        {
            return AuthApps.Select(x => x.ToEntity()).ToList();
        }

        public static AuthAppEntity ToEntity(this AuthApp contract, AuthAppEntity entity = null)
        {
            var AuthAppEntity = entity ?? new();

            AuthAppEntity.IsDeleted= contract.IsDeleted;
AuthAppEntity.Id= contract.Id;
AuthAppEntity.Name= contract.Name;
AuthAppEntity.Notes= contract.Notes;

            AuthAppEntity.BfsSystemId= contract.BfsSystemId;

            return AuthAppEntity;
        }     
    }
}
