using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class SspTransactionMapper
    {
        public static SspTransaction ToContract(this SspTransactionEntity entity)
        {
            var contract = new SspTransaction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
SourceDate= entity.SourceDate,
TransactionDate= entity.TransactionDate,
Source= entity.Source,
Quantity= entity.Quantity,
Price= entity.Price,
ToQuantity= entity.ToQuantity,

               SsPortfolioId= entity.SsPortfolioId,
TransactionTypeId= entity.TransactionTypeId,
StockShareId= entity.StockShareId,
ToPortfolioId= entity.ToPortfolioId,

            };

            return contract;
        }

        public static List<SspTransaction> ToContract(this IEnumerable<SspTransactionEntity> SspTransactions)
        {
            return SspTransactions.Select(x => x.ToContract()).ToList();
        }

        public static List<SspTransactionEntity> ToEntity(this IEnumerable<SspTransaction> SspTransactions)
        {
            return SspTransactions.Select(x => x.ToEntity()).ToList();
        }

        public static SspTransactionEntity ToEntity(this SspTransaction contract, SspTransactionEntity entity = null)
        {
            var SspTransactionEntity = entity ?? new();

            SspTransactionEntity.IsDeleted= contract.IsDeleted;
SspTransactionEntity.Id= contract.Id;
SspTransactionEntity.Name= contract.Name;
SspTransactionEntity.Notes= contract.Notes;
SspTransactionEntity.SourceDate= contract.SourceDate;
SspTransactionEntity.TransactionDate= contract.TransactionDate;
SspTransactionEntity.Source= contract.Source;
SspTransactionEntity.Quantity= contract.Quantity;
SspTransactionEntity.Price= contract.Price;
SspTransactionEntity.ToQuantity= contract.ToQuantity;

            SspTransactionEntity.SsPortfolioId= contract.SsPortfolioId;
SspTransactionEntity.TransactionTypeId= contract.TransactionTypeId;
SspTransactionEntity.StockShareId= contract.StockShareId;
SspTransactionEntity.ToPortfolioId= contract.ToPortfolioId;

            return SspTransactionEntity;
        }     
    }
}

