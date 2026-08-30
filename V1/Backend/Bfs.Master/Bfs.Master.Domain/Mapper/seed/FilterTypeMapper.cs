using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class FilterTypeMapper
    {
        public static FilterType ToContract(this FilterTypeEntity entity)
        {
            var contract = new FilterType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<FilterType> ToContract(this IEnumerable<FilterTypeEntity> FilterTypes)
        {
            return FilterTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<FilterTypeEntity> ToEntity(this IEnumerable<FilterType> FilterTypes)
        {
            return FilterTypes.Select(x => x.ToEntity()).ToList();
        }

        public static FilterTypeEntity ToEntity(this FilterType contract, FilterTypeEntity entity = null)
        {
            var FilterTypeEntity = entity ?? new();

            FilterTypeEntity.IsDeleted= contract.IsDeleted;
FilterTypeEntity.Id= contract.Id;
FilterTypeEntity.Name= contract.Name;
FilterTypeEntity.Notes= contract.Notes;

            return FilterTypeEntity;
        }     
    }
}
