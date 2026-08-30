using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class ChartElementMapper
    {
        public static ChartElement ToContract(this ChartElementEntity entity)
        {
            var contract = new ChartElement()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<ChartElement> ToContract(this IEnumerable<ChartElementEntity> ChartElements)
        {
            return ChartElements.Select(x => x.ToContract()).ToList();
        }

        public static List<ChartElementEntity> ToEntity(this IEnumerable<ChartElement> ChartElements)
        {
            return ChartElements.Select(x => x.ToEntity()).ToList();
        }

        public static ChartElementEntity ToEntity(this ChartElement contract, ChartElementEntity entity = null)
        {
            var ChartElementEntity = entity ?? new();

            ChartElementEntity.IsDeleted= contract.IsDeleted;
ChartElementEntity.Id= contract.Id;
ChartElementEntity.Name= contract.Name;
ChartElementEntity.Notes= contract.Notes;

            return ChartElementEntity;
        }     
    }
}
