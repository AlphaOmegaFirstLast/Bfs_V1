using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class SspStockMapper
    {
        public static SspStock ToContract(this SspStockEntity entity)
        {
            var contract = new SspStock()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
Quantity= entity.Quantity,
AverageCost= entity.AverageCost,

               SsPortfolioId= entity.SsPortfolioId,
StockShareId= entity.StockShareId,

            };

            return contract;
        }

        public static List<SspStock> ToContract(this IEnumerable<SspStockEntity> SspStocks)
        {
            return SspStocks.Select(x => x.ToContract()).ToList();
        }

        public static List<SspStockEntity> ToEntity(this IEnumerable<SspStock> SspStocks)
        {
            return SspStocks.Select(x => x.ToEntity()).ToList();
        }

        public static SspStockEntity ToEntity(this SspStock contract, SspStockEntity entity = null)
        {
            var SspStockEntity = entity ?? new();

            SspStockEntity.IsDeleted= contract.IsDeleted;
SspStockEntity.Id= contract.Id;
SspStockEntity.Name= contract.Name;
SspStockEntity.Notes= contract.Notes;
SspStockEntity.Quantity= contract.Quantity;
SspStockEntity.AverageCost= contract.AverageCost;

            SspStockEntity.SsPortfolioId= contract.SsPortfolioId;
SspStockEntity.StockShareId= contract.StockShareId;

            return SspStockEntity;
        }     
    }
}

