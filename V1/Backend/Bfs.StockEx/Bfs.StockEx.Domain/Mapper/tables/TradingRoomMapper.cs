using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class TradingRoomMapper
    {
        public static TradingRoom ToContract(this TradingRoomEntity entity)
        {
            var contract = new TradingRoom()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,

            };

            return contract;
        }

        public static List<TradingRoom> ToContract(this IEnumerable<TradingRoomEntity> TradingRooms)
        {
            return TradingRooms.Select(x => x.ToContract()).ToList();
        }

        public static List<TradingRoomEntity> ToEntity(this IEnumerable<TradingRoom> TradingRooms)
        {
            return TradingRooms.Select(x => x.ToEntity()).ToList();
        }

        public static TradingRoomEntity ToEntity(this TradingRoom contract, TradingRoomEntity entity = null)
        {
            var TradingRoomEntity = entity ?? new();

            TradingRoomEntity.IsDeleted= contract.IsDeleted;
TradingRoomEntity.Id= contract.Id;
TradingRoomEntity.Name= contract.Name;

            return TradingRoomEntity;
        }     
    }
}

