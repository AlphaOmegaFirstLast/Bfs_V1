using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class CalculationMethodMapper
    {
        public static CalculationMethod ToContract(this CalculationMethodEntity entity)
        {
            var contract = new CalculationMethod()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<CalculationMethod> ToContract(this IEnumerable<CalculationMethodEntity> CalculationMethods)
        {
            return CalculationMethods.Select(x => x.ToContract()).ToList();
        }

        public static List<CalculationMethodEntity> ToEntity(this IEnumerable<CalculationMethod> CalculationMethods)
        {
            return CalculationMethods.Select(x => x.ToEntity()).ToList();
        }

        public static CalculationMethodEntity ToEntity(this CalculationMethod contract, CalculationMethodEntity entity = null)
        {
            var CalculationMethodEntity = entity ?? new();

            CalculationMethodEntity.IsDeleted= contract.IsDeleted;
CalculationMethodEntity.Id= contract.Id;
CalculationMethodEntity.Name= contract.Name;
CalculationMethodEntity.Notes= contract.Notes;

            return CalculationMethodEntity;
        }     
    }
}

