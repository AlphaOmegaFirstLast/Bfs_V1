using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class StrTransactionMapper
    {
        public static StrTransaction ToContract(this StrTransactionEntity entity)
        {
            var contract = new StrTransaction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Quantity= entity.Quantity,
Notes= entity.Notes,

               StrStoreId= entity.StrStoreId,
StrOperationId= entity.StrOperationId,
StrProductId= entity.StrProductId,

            };

            return contract;
        }

        public static List<StrTransaction> ToContract(this IEnumerable<StrTransactionEntity> StrTransactions)
        {
            return StrTransactions.Select(x => x.ToContract()).ToList();
        }

        public static List<StrTransactionEntity> ToEntity(this IEnumerable<StrTransaction> StrTransactions)
        {
            return StrTransactions.Select(x => x.ToEntity()).ToList();
        }

        public static StrTransactionEntity ToEntity(this StrTransaction contract, StrTransactionEntity entity = null)
        {
            var StrTransactionEntity = entity ?? new();

            StrTransactionEntity.IsDeleted= contract.IsDeleted;
StrTransactionEntity.Id= contract.Id;
StrTransactionEntity.Quantity= contract.Quantity;
StrTransactionEntity.Notes= contract.Notes;

            StrTransactionEntity.StrStoreId= contract.StrStoreId;
StrTransactionEntity.StrOperationId= contract.StrOperationId;
StrTransactionEntity.StrProductId= contract.StrProductId;

            return StrTransactionEntity;
        }     
    }
}
