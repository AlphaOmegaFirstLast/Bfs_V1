using Bfs.Core.ObjectFields;
using Bfs.Auth.Contracts;
using Bfs.Auth.Data.Models;

namespace Bfs.Auth.Domain.Mapper
{
    public static class AppMapper
    {
        public static App ToContract(this AppEntity entity)
        {
            var contract = new App()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
Logo= entity.Logo,

               BfsSystemId= entity.BfsSystemId,

            };

            return contract;
        }

        public static List<App> ToContract(this IEnumerable<AppEntity> Apps)
        {
            return Apps.Select(x => x.ToContract()).ToList();
        }

        public static List<AppEntity> ToEntity(this IEnumerable<App> Apps)
        {
            return Apps.Select(x => x.ToEntity()).ToList();
        }

        public static AppEntity ToEntity(this App contract, AppEntity entity = null)
        {
            var AppEntity = entity ?? new();

            AppEntity.IsDeleted= contract.IsDeleted;
AppEntity.Id= contract.Id;
AppEntity.Name= contract.Name;
AppEntity.Notes= contract.Notes;
AppEntity.Logo= contract.Logo;

            AppEntity.BfsSystemId= contract.BfsSystemId;

            return AppEntity;
        }     
    }
}

