using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class CurrentPriceMapper
    {
        public static CurrentPrice ToContract(this CurrentPriceEntity entity)
        {
            var contract = new CurrentPrice()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
TransactionDate= entity.TransactionDate,
Price= entity.Price,

               StockShareId= entity.StockShareId,

            };

            return contract;
        }

        public static List<CurrentPrice> ToContract(this IEnumerable<CurrentPriceEntity> CurrentPrices)
        {
            return CurrentPrices.Select(x => x.ToContract()).ToList();
        }

        public static List<CurrentPriceEntity> ToEntity(this IEnumerable<CurrentPrice> CurrentPrices)
        {
            return CurrentPrices.Select(x => x.ToEntity()).ToList();
        }

        public static CurrentPriceEntity ToEntity(this CurrentPrice contract, CurrentPriceEntity entity = null)
        {
            var CurrentPriceEntity = entity ?? new();

            CurrentPriceEntity.IsDeleted= contract.IsDeleted;
CurrentPriceEntity.Id= contract.Id;
CurrentPriceEntity.Name= contract.Name;
CurrentPriceEntity.Notes= contract.Notes;
CurrentPriceEntity.TransactionDate= contract.TransactionDate;
CurrentPriceEntity.Price= contract.Price;

            CurrentPriceEntity.StockShareId= contract.StockShareId;

            return CurrentPriceEntity;
        }     
    }
}

