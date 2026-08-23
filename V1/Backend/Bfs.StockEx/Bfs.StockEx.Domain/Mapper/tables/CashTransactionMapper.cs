using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class CashTransactionMapper
    {
        public static CashTransaction ToContract(this CashTransactionEntity entity)
        {
            var contract = new CashTransaction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
Source= entity.Source,
SourceDate= entity.SourceDate,
TransactionDate= entity.TransactionDate,
Value= entity.Value,

               SspTransactionId= entity.SspTransactionId,
SsPortfolioId= entity.SsPortfolioId,
TransactionTypeId= entity.TransactionTypeId,
ExpensesTypeId= entity.ExpensesTypeId,

            };

            return contract;
        }

        public static List<CashTransaction> ToContract(this IEnumerable<CashTransactionEntity> CashTransactions)
        {
            return CashTransactions.Select(x => x.ToContract()).ToList();
        }

        public static List<CashTransactionEntity> ToEntity(this IEnumerable<CashTransaction> CashTransactions)
        {
            return CashTransactions.Select(x => x.ToEntity()).ToList();
        }

        public static CashTransactionEntity ToEntity(this CashTransaction contract, CashTransactionEntity entity = null)
        {
            var CashTransactionEntity = entity ?? new();

            CashTransactionEntity.IsDeleted= contract.IsDeleted;
CashTransactionEntity.Id= contract.Id;
CashTransactionEntity.Name= contract.Name;
CashTransactionEntity.Notes= contract.Notes;
CashTransactionEntity.Source= contract.Source;
CashTransactionEntity.SourceDate= contract.SourceDate;
CashTransactionEntity.TransactionDate= contract.TransactionDate;
CashTransactionEntity.Value= contract.Value;

            CashTransactionEntity.SspTransactionId= contract.SspTransactionId;
CashTransactionEntity.SsPortfolioId= contract.SsPortfolioId;
CashTransactionEntity.TransactionTypeId= contract.TransactionTypeId;
CashTransactionEntity.ExpensesTypeId= contract.ExpensesTypeId;

            return CashTransactionEntity;
        }     
    }
}

