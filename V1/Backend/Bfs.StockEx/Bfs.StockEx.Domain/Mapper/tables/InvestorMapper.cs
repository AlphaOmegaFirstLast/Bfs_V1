using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class InvestorMapper
    {
        public static Investor ToContract(this InvestorEntity entity)
        {
            var contract = new Investor()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
Code= entity.Code,
Email= entity.Email,

            };

            return contract;
        }

        public static List<Investor> ToContract(this IEnumerable<InvestorEntity> Investors)
        {
            return Investors.Select(x => x.ToContract()).ToList();
        }

        public static List<InvestorEntity> ToEntity(this IEnumerable<Investor> Investors)
        {
            return Investors.Select(x => x.ToEntity()).ToList();
        }

        public static InvestorEntity ToEntity(this Investor contract, InvestorEntity entity = null)
        {
            var InvestorEntity = entity ?? new();

            InvestorEntity.IsDeleted= contract.IsDeleted;
InvestorEntity.Id= contract.Id;
InvestorEntity.Name= contract.Name;
InvestorEntity.Notes= contract.Notes;
InvestorEntity.Code= contract.Code;
InvestorEntity.Email= contract.Email;

            return InvestorEntity;
        }     
    }
}

