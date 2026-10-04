using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class CreditInterestMapper
    {
        public static CreditInterest ToContract(this CreditInterestEntity entity)
        {
            var contract = new CreditInterest()
            {
               Id= entity.Id,
Name= entity.Name,
InterestPercent= entity.InterestPercent,
Notes= entity.Notes,
IsDeleted= entity.IsDeleted,

               PaymentModeId= entity.PaymentModeId,
BrokerAgreementId= entity.BrokerAgreementId,

            };

            return contract;
        }

        public static List<CreditInterest> ToContract(this IEnumerable<CreditInterestEntity> CreditInterests)
        {
            return CreditInterests.Select(x => x.ToContract()).ToList();
        }

        public static List<CreditInterestEntity> ToEntity(this IEnumerable<CreditInterest> CreditInterests)
        {
            return CreditInterests.Select(x => x.ToEntity()).ToList();
        }

        public static CreditInterestEntity ToEntity(this CreditInterest contract, CreditInterestEntity entity = null)
        {
            var CreditInterestEntity = entity ?? new();

            CreditInterestEntity.Id= contract.Id;
CreditInterestEntity.Name= contract.Name;
CreditInterestEntity.InterestPercent= contract.InterestPercent;
CreditInterestEntity.Notes= contract.Notes;
CreditInterestEntity.IsDeleted= contract.IsDeleted;

            CreditInterestEntity.PaymentModeId= contract.PaymentModeId;
CreditInterestEntity.BrokerAgreementId= contract.BrokerAgreementId;

            return CreditInterestEntity;
        }     
    }
}

