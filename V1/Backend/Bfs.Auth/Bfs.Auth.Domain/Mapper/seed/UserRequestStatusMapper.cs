using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class UserRequestStatusMapper
    {
        public static UserRequestStatus ToContract(this UserRequestStatusEntity entity)
        {
            var contract = new UserRequestStatus()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<UserRequestStatus> ToContract(this IEnumerable<UserRequestStatusEntity> UserRequestStatuss)
        {
            return UserRequestStatuss.Select(x => x.ToContract()).ToList();
        }

        public static List<UserRequestStatusEntity> ToEntity(this IEnumerable<UserRequestStatus> UserRequestStatuss)
        {
            return UserRequestStatuss.Select(x => x.ToEntity()).ToList();
        }

        public static UserRequestStatusEntity ToEntity(this UserRequestStatus contract, UserRequestStatusEntity entity = null)
        {
            var UserRequestStatusEntity = entity ?? new();

            UserRequestStatusEntity.IsDeleted= contract.IsDeleted;
UserRequestStatusEntity.Id= contract.Id;
UserRequestStatusEntity.Name= contract.Name;
UserRequestStatusEntity.Notes= contract.Notes;

            return UserRequestStatusEntity;
        }     
    }
}
