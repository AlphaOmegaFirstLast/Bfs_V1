using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class OverdraftPortfolioMapper
    {
        public static OverdraftPortfolio ToContract(this OverdraftPortfolioEntity entity)
        {
            var contract = new OverdraftPortfolio()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
OverdraftValue= entity.OverdraftValue,

               SsPortfolioId= entity.SsPortfolioId,

            };

            return contract;
        }

        public static List<OverdraftPortfolio> ToContract(this IEnumerable<OverdraftPortfolioEntity> OverdraftPortfolios)
        {
            return OverdraftPortfolios.Select(x => x.ToContract()).ToList();
        }

        public static List<OverdraftPortfolioEntity> ToEntity(this IEnumerable<OverdraftPortfolio> OverdraftPortfolios)
        {
            return OverdraftPortfolios.Select(x => x.ToEntity()).ToList();
        }

        public static OverdraftPortfolioEntity ToEntity(this OverdraftPortfolio contract, OverdraftPortfolioEntity entity = null)
        {
            var OverdraftPortfolioEntity = entity ?? new();

            OverdraftPortfolioEntity.IsDeleted= contract.IsDeleted;
OverdraftPortfolioEntity.Id= contract.Id;
OverdraftPortfolioEntity.Name= contract.Name;
OverdraftPortfolioEntity.Notes= contract.Notes;
OverdraftPortfolioEntity.OverdraftValue= contract.OverdraftValue;

            OverdraftPortfolioEntity.SsPortfolioId= contract.SsPortfolioId;

            return OverdraftPortfolioEntity;
        }     
    }
}

