using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class CurrencyMapper
    {
        public static Currency ToContract(this CurrencyEntity entity)
        {
            var contract = new Currency()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<Currency> ToContract(this IEnumerable<CurrencyEntity> Currencys)
        {
            return Currencys.Select(x => x.ToContract()).ToList();
        }

        public static List<CurrencyEntity> ToEntity(this IEnumerable<Currency> Currencys)
        {
            return Currencys.Select(x => x.ToEntity()).ToList();
        }

        public static CurrencyEntity ToEntity(this Currency contract, CurrencyEntity entity = null)
        {
            var CurrencyEntity = entity ?? new();

            CurrencyEntity.IsDeleted= contract.IsDeleted;
CurrencyEntity.Id= contract.Id;
CurrencyEntity.Name= contract.Name;
CurrencyEntity.Notes= contract.Notes;

            return CurrencyEntity;
        }     
    }
}

