using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class SourceTypeMapper
    {
        public static SourceType ToContract(this SourceTypeEntity entity)
        {
            var contract = new SourceType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<SourceType> ToContract(this IEnumerable<SourceTypeEntity> SourceTypes)
        {
            return SourceTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<SourceTypeEntity> ToEntity(this IEnumerable<SourceType> SourceTypes)
        {
            return SourceTypes.Select(x => x.ToEntity()).ToList();
        }

        public static SourceTypeEntity ToEntity(this SourceType contract, SourceTypeEntity entity = null)
        {
            var SourceTypeEntity = entity ?? new();

            SourceTypeEntity.IsDeleted= contract.IsDeleted;
SourceTypeEntity.Id= contract.Id;
SourceTypeEntity.Name= contract.Name;
SourceTypeEntity.Notes= contract.Notes;

            return SourceTypeEntity;
        }     
    }
}

