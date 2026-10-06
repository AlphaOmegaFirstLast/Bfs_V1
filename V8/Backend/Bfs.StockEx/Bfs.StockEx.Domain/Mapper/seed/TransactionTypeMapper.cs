using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class TransactionTypeMapper
    {
        public static TransactionType ToContract(this TransactionTypeEntity entity)
        {
            var contract = new TransactionType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

               EffectTypeId= entity.EffectTypeId,
StockEntityTypeId= entity.StockEntityTypeId,
CalculationMethodId= entity.CalculationMethodId,
SourceTypeId= entity.SourceTypeId,
StockFieldTypeId= entity.StockFieldTypeId,
NextTransactionTypeId= entity.NextTransactionTypeId,

            };

            return contract;
        }

        public static List<TransactionType> ToContract(this IEnumerable<TransactionTypeEntity> TransactionTypes)
        {
            return TransactionTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<TransactionTypeEntity> ToEntity(this IEnumerable<TransactionType> TransactionTypes)
        {
            return TransactionTypes.Select(x => x.ToEntity()).ToList();
        }

        public static TransactionTypeEntity ToEntity(this TransactionType contract, TransactionTypeEntity entity = null)
        {
            var TransactionTypeEntity = entity ?? new();

            TransactionTypeEntity.IsDeleted= contract.IsDeleted;
TransactionTypeEntity.Id= contract.Id;
TransactionTypeEntity.Name= contract.Name;
TransactionTypeEntity.Notes= contract.Notes;

            TransactionTypeEntity.EffectTypeId= contract.EffectTypeId;
TransactionTypeEntity.StockEntityTypeId= contract.StockEntityTypeId;
TransactionTypeEntity.CalculationMethodId= contract.CalculationMethodId;
TransactionTypeEntity.SourceTypeId= contract.SourceTypeId;
TransactionTypeEntity.StockFieldTypeId= contract.StockFieldTypeId;
TransactionTypeEntity.NextTransactionTypeId= contract.NextTransactionTypeId;

            return TransactionTypeEntity;
        }     
    }
}

