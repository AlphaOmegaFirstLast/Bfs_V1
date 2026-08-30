using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class AggregateTypeMapper
    {
        public static AggregateType ToContract(this AggregateTypeEntity entity)
        {
            var contract = new AggregateType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<AggregateType> ToContract(this IEnumerable<AggregateTypeEntity> AggregateTypes)
        {
            return AggregateTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<AggregateTypeEntity> ToEntity(this IEnumerable<AggregateType> AggregateTypes)
        {
            return AggregateTypes.Select(x => x.ToEntity()).ToList();
        }

        public static AggregateTypeEntity ToEntity(this AggregateType contract, AggregateTypeEntity entity = null)
        {
            var AggregateTypeEntity = entity ?? new();

            AggregateTypeEntity.IsDeleted= contract.IsDeleted;
AggregateTypeEntity.Id= contract.Id;
AggregateTypeEntity.Name= contract.Name;
AggregateTypeEntity.Notes= contract.Notes;

            return AggregateTypeEntity;
        }     
    }
}
