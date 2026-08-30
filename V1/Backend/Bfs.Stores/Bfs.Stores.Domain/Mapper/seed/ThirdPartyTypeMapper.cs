using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class ThirdPartyTypeMapper
    {
        public static ThirdPartyType ToContract(this ThirdPartyTypeEntity entity)
        {
            var contract = new ThirdPartyType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<ThirdPartyType> ToContract(this IEnumerable<ThirdPartyTypeEntity> ThirdPartyTypes)
        {
            return ThirdPartyTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<ThirdPartyTypeEntity> ToEntity(this IEnumerable<ThirdPartyType> ThirdPartyTypes)
        {
            return ThirdPartyTypes.Select(x => x.ToEntity()).ToList();
        }

        public static ThirdPartyTypeEntity ToEntity(this ThirdPartyType contract, ThirdPartyTypeEntity entity = null)
        {
            var ThirdPartyTypeEntity = entity ?? new();

            ThirdPartyTypeEntity.IsDeleted= contract.IsDeleted;
ThirdPartyTypeEntity.Id= contract.Id;
ThirdPartyTypeEntity.Name= contract.Name;
ThirdPartyTypeEntity.Notes= contract.Notes;

            return ThirdPartyTypeEntity;
        }     
    }
}

