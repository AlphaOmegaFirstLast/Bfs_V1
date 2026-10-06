using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class StockShareMapper
    {
        public static StockShare ToContract(this StockShareEntity entity)
        {
            var contract = new StockShare()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

               TradingRoomId= entity.TradingRoomId,
CurrencyId= entity.CurrencyId,

            };

            return contract;
        }

        public static List<StockShare> ToContract(this IEnumerable<StockShareEntity> StockShares)
        {
            return StockShares.Select(x => x.ToContract()).ToList();
        }

        public static List<StockShareEntity> ToEntity(this IEnumerable<StockShare> StockShares)
        {
            return StockShares.Select(x => x.ToEntity()).ToList();
        }

        public static StockShareEntity ToEntity(this StockShare contract, StockShareEntity entity = null)
        {
            var StockShareEntity = entity ?? new();

            StockShareEntity.IsDeleted= contract.IsDeleted;
StockShareEntity.Id= contract.Id;
StockShareEntity.Name= contract.Name;
StockShareEntity.Notes= contract.Notes;

            StockShareEntity.TradingRoomId= contract.TradingRoomId;
StockShareEntity.CurrencyId= contract.CurrencyId;

            return StockShareEntity;
        }     
    }
}

