using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class AuthUserMapper
    {
        public static AuthUser ToContract(this AuthUserEntity entity)
        {
            var contract = new AuthUser()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
AspNetUserId= entity.AspNetUserId,
Notes= entity.Notes,
Name= entity.Name,

            };

            return contract;
        }

        public static List<AuthUser> ToContract(this IEnumerable<AuthUserEntity> AuthUsers)
        {
            return AuthUsers.Select(x => x.ToContract()).ToList();
        }

        public static List<AuthUserEntity> ToEntity(this IEnumerable<AuthUser> AuthUsers)
        {
            return AuthUsers.Select(x => x.ToEntity()).ToList();
        }

        public static AuthUserEntity ToEntity(this AuthUser contract, AuthUserEntity entity = null)
        {
            var AuthUserEntity = entity ?? new();

            AuthUserEntity.IsDeleted= contract.IsDeleted;
AuthUserEntity.Id= contract.Id;
AuthUserEntity.AspNetUserId= contract.AspNetUserId;
AuthUserEntity.Notes= contract.Notes;
AuthUserEntity.Name= contract.Name;

            return AuthUserEntity;
        }     
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

