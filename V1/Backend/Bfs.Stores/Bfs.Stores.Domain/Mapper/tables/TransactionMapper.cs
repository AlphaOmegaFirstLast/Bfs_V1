using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class TransactionMapper
    {
        public static Transaction ToContract(this TransactionEntity entity)
        {
            var contract = new Transaction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Quantity= entity.Quantity,
Notes= entity.Notes,

               StoreId= entity.StoreId,
OperationId= entity.OperationId,
ProductId= entity.ProductId,

            };

            return contract;
        }

        public static List<Transaction> ToContract(this IEnumerable<TransactionEntity> Transactions)
        {
            return Transactions.Select(x => x.ToContract()).ToList();
        }

        public static List<TransactionEntity> ToEntity(this IEnumerable<Transaction> Transactions)
        {
            return Transactions.Select(x => x.ToEntity()).ToList();
        }

        public static TransactionEntity ToEntity(this Transaction contract, TransactionEntity entity = null)
        {
            var TransactionEntity = entity ?? new();

            TransactionEntity.IsDeleted= contract.IsDeleted;
TransactionEntity.Id= contract.Id;
TransactionEntity.Quantity= contract.Quantity;
TransactionEntity.Notes= contract.Notes;

            TransactionEntity.StoreId= contract.StoreId;
TransactionEntity.OperationId= contract.OperationId;
TransactionEntity.ProductId= contract.ProductId;

            return TransactionEntity;
        }     
    }
}

