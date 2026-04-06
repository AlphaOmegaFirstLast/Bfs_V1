using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class UserRequestMapper
    {
        public static UserRequest ToContract(this UserRequestEntity entity)
        {
            var contract = new UserRequest()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
AspNetUserId= entity.AspNetUserId,
Notes= entity.Notes,
Name= entity.Name,
Email= entity.Email,

            };

            return contract;
        }

        public static List<UserRequest> ToContract(this IEnumerable<UserRequestEntity> UserRequests)
        {
            return UserRequests.Select(x => x.ToContract()).ToList();
        }

        public static List<UserRequestEntity> ToEntity(this IEnumerable<UserRequest> UserRequests)
        {
            return UserRequests.Select(x => x.ToEntity()).ToList();
        }

        public static UserRequestEntity ToEntity(this UserRequest contract, UserRequestEntity entity = null)
        {
            var UserRequestEntity = entity ?? new();

            UserRequestEntity.IsDeleted= contract.IsDeleted;
UserRequestEntity.Id= contract.Id;
UserRequestEntity.AspNetUserId= contract.AspNetUserId;
UserRequestEntity.Notes= contract.Notes;
UserRequestEntity.Name= contract.Name;
UserRequestEntity.Email= contract.Email;

            return UserRequestEntity;
        }     
    }
}

