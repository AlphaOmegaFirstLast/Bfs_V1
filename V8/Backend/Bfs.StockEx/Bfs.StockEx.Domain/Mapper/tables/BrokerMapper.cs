using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class BrokerMapper
    {
        public static Broker ToContract(this BrokerEntity entity)
        {
            var contract = new Broker()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
Code= entity.Code,
Email= entity.Email,

               TradingRoomId= entity.TradingRoomId,

            };

            return contract;
        }

        public static List<Broker> ToContract(this IEnumerable<BrokerEntity> Brokers)
        {
            return Brokers.Select(x => x.ToContract()).ToList();
        }

        public static List<BrokerEntity> ToEntity(this IEnumerable<Broker> Brokers)
        {
            return Brokers.Select(x => x.ToEntity()).ToList();
        }

        public static BrokerEntity ToEntity(this Broker contract, BrokerEntity entity = null)
        {
            var BrokerEntity = entity ?? new();

            BrokerEntity.IsDeleted= contract.IsDeleted;
BrokerEntity.Id= contract.Id;
BrokerEntity.Name= contract.Name;
BrokerEntity.Notes= contract.Notes;
BrokerEntity.Code= contract.Code;
BrokerEntity.Email= contract.Email;

            BrokerEntity.TradingRoomId= contract.TradingRoomId;

            return BrokerEntity;
        }     
    }
}

