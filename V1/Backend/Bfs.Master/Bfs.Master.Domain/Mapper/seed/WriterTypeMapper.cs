using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class WriterTypeMapper
    {
        public static WriterType ToContract(this WriterTypeEntity entity)
        {
            var contract = new WriterType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<WriterType> ToContract(this IEnumerable<WriterTypeEntity> WriterTypes)
        {
            return WriterTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<WriterTypeEntity> ToEntity(this IEnumerable<WriterType> WriterTypes)
        {
            return WriterTypes.Select(x => x.ToEntity()).ToList();
        }

        public static WriterTypeEntity ToEntity(this WriterType contract, WriterTypeEntity entity = null)
        {
            var WriterTypeEntity = entity ?? new();

            WriterTypeEntity.IsDeleted= contract.IsDeleted;
WriterTypeEntity.Id= contract.Id;
WriterTypeEntity.Name= contract.Name;
WriterTypeEntity.Notes= contract.Notes;

            return WriterTypeEntity;
        }     
    }
}
