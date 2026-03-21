using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class AuthRoleComponentSystemActionMapper
    {
        public static AuthRoleComponentSystemAction ToContract(this AuthRoleComponentSystemActionEntity entity)
        {
            var contract = new AuthRoleComponentSystemAction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               BfsComponentId= entity.BfsComponentId,
SystemActionId= entity.SystemActionId,
AuthRoleId= entity.AuthRoleId,

            };

            return contract;
        }

        public static List<AuthRoleComponentSystemAction> ToContract(this IEnumerable<AuthRoleComponentSystemActionEntity> AuthRoleComponentSystemActions)
        {
            return AuthRoleComponentSystemActions.Select(x => x.ToContract()).ToList();
        }

        public static List<AuthRoleComponentSystemActionEntity> ToEntity(this IEnumerable<AuthRoleComponentSystemAction> AuthRoleComponentSystemActions)
        {
            return AuthRoleComponentSystemActions.Select(x => x.ToEntity()).ToList();
        }

        public static AuthRoleComponentSystemActionEntity ToEntity(this AuthRoleComponentSystemAction contract, AuthRoleComponentSystemActionEntity entity = null)
        {
            var AuthRoleComponentSystemActionEntity = entity ?? new();

            AuthRoleComponentSystemActionEntity.IsDeleted= contract.IsDeleted;
AuthRoleComponentSystemActionEntity.Id= contract.Id;

            AuthRoleComponentSystemActionEntity.BfsComponentId= contract.BfsComponentId;
AuthRoleComponentSystemActionEntity.SystemActionId= contract.SystemActionId;
AuthRoleComponentSystemActionEntity.AuthRoleId= contract.AuthRoleId;

            return AuthRoleComponentSystemActionEntity;
        }     
    }
}
