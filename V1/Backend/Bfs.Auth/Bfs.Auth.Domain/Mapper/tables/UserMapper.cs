using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class UserMapper
    {
        public static User ToContract(this UserEntity entity)
        {
            var contract = new User()
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

        public static List<User> ToContract(this IEnumerable<UserEntity> Users)
        {
            return Users.Select(x => x.ToContract()).ToList();
        }

        public static List<UserEntity> ToEntity(this IEnumerable<User> Users)
        {
            return Users.Select(x => x.ToEntity()).ToList();
        }

        public static UserEntity ToEntity(this User contract, UserEntity entity = null)
        {
            var UserEntity = entity ?? new();

            UserEntity.IsDeleted= contract.IsDeleted;
UserEntity.Id= contract.Id;
UserEntity.AspNetUserId= contract.AspNetUserId;
UserEntity.Notes= contract.Notes;
UserEntity.Name= contract.Name;
UserEntity.Email= contract.Email;

            return UserEntity;
        }     
    }
}

