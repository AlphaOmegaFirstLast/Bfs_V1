using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class SsPortfolioBalanceMapper
    {
        public static SsPortfolioBalance ToContract(this SsPortfolioBalanceEntity entity)
        {
            var contract = new SsPortfolioBalance()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
Balance= entity.Balance,

               SsPortfolioId= entity.SsPortfolioId,
CurrencyId= entity.CurrencyId,

            };

            return contract;
        }

        public static List<SsPortfolioBalance> ToContract(this IEnumerable<SsPortfolioBalanceEntity> SsPortfolioBalances)
        {
            return SsPortfolioBalances.Select(x => x.ToContract()).ToList();
        }

        public static List<SsPortfolioBalanceEntity> ToEntity(this IEnumerable<SsPortfolioBalance> SsPortfolioBalances)
        {
            return SsPortfolioBalances.Select(x => x.ToEntity()).ToList();
        }

        public static SsPortfolioBalanceEntity ToEntity(this SsPortfolioBalance contract, SsPortfolioBalanceEntity entity = null)
        {
            var SsPortfolioBalanceEntity = entity ?? new();

            SsPortfolioBalanceEntity.IsDeleted= contract.IsDeleted;
SsPortfolioBalanceEntity.Id= contract.Id;
SsPortfolioBalanceEntity.Name= contract.Name;
SsPortfolioBalanceEntity.Notes= contract.Notes;
SsPortfolioBalanceEntity.Balance= contract.Balance;

            SsPortfolioBalanceEntity.SsPortfolioId= contract.SsPortfolioId;
SsPortfolioBalanceEntity.CurrencyId= contract.CurrencyId;

            return SsPortfolioBalanceEntity;
        }     
    }
}

