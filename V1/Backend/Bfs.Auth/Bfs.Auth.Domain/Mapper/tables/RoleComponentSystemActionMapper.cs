using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class RoleComponentSystemActionMapper
    {
        public static RoleComponentSystemAction ToContract(this RoleComponentSystemActionEntity entity)
        {
            var contract = new RoleComponentSystemAction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,

               BfsComponentId= entity.BfsComponentId,
SystemActionId= entity.SystemActionId,
RoleId= entity.RoleId,

            };

            return contract;
        }

        public static List<RoleComponentSystemAction> ToContract(this IEnumerable<RoleComponentSystemActionEntity> RoleComponentSystemActions)
        {
            return RoleComponentSystemActions.Select(x => x.ToContract()).ToList();
        }

        public static List<RoleComponentSystemActionEntity> ToEntity(this IEnumerable<RoleComponentSystemAction> RoleComponentSystemActions)
        {
            return RoleComponentSystemActions.Select(x => x.ToEntity()).ToList();
        }

        public static RoleComponentSystemActionEntity ToEntity(this RoleComponentSystemAction contract, RoleComponentSystemActionEntity entity = null)
        {
            var RoleComponentSystemActionEntity = entity ?? new();

            RoleComponentSystemActionEntity.IsDeleted= contract.IsDeleted;
RoleComponentSystemActionEntity.Id= contract.Id;

            RoleComponentSystemActionEntity.BfsComponentId= contract.BfsComponentId;
RoleComponentSystemActionEntity.SystemActionId= contract.SystemActionId;
RoleComponentSystemActionEntity.RoleId= contract.RoleId;

            return RoleComponentSystemActionEntity;
        }     
    }
}

