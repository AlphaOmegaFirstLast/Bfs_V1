using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class ExpensesTypeMapper
    {
        public static ExpensesType ToContract(this ExpensesTypeEntity entity)
        {
            var contract = new ExpensesType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<ExpensesType> ToContract(this IEnumerable<ExpensesTypeEntity> ExpensesTypes)
        {
            return ExpensesTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<ExpensesTypeEntity> ToEntity(this IEnumerable<ExpensesType> ExpensesTypes)
        {
            return ExpensesTypes.Select(x => x.ToEntity()).ToList();
        }

        public static ExpensesTypeEntity ToEntity(this ExpensesType contract, ExpensesTypeEntity entity = null)
        {
            var ExpensesTypeEntity = entity ?? new();

            ExpensesTypeEntity.IsDeleted= contract.IsDeleted;
ExpensesTypeEntity.Id= contract.Id;
ExpensesTypeEntity.Name= contract.Name;
ExpensesTypeEntity.Notes= contract.Notes;

            return ExpensesTypeEntity;
        }     
    }
}

