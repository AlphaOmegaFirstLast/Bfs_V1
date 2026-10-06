using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class SsPortfolioMapper
    {
        public static SsPortfolio ToContract(this SsPortfolioEntity entity)
        {
            var contract = new SsPortfolio()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

               BrokerId= entity.BrokerId,
InvestorId= entity.InvestorId,

            };

            return contract;
        }

        public static List<SsPortfolio> ToContract(this IEnumerable<SsPortfolioEntity> SsPortfolios)
        {
            return SsPortfolios.Select(x => x.ToContract()).ToList();
        }

        public static List<SsPortfolioEntity> ToEntity(this IEnumerable<SsPortfolio> SsPortfolios)
        {
            return SsPortfolios.Select(x => x.ToEntity()).ToList();
        }

        public static SsPortfolioEntity ToEntity(this SsPortfolio contract, SsPortfolioEntity entity = null)
        {
            var SsPortfolioEntity = entity ?? new();

            SsPortfolioEntity.IsDeleted= contract.IsDeleted;
SsPortfolioEntity.Id= contract.Id;
SsPortfolioEntity.Name= contract.Name;
SsPortfolioEntity.Notes= contract.Notes;

            SsPortfolioEntity.BrokerId= contract.BrokerId;
SsPortfolioEntity.InvestorId= contract.InvestorId;

            return SsPortfolioEntity;
        }     
    }
}

