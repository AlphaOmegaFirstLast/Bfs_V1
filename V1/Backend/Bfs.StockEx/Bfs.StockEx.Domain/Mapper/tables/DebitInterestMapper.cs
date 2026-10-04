using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class DebitInterestMapper
    {
        public static DebitInterest ToContract(this DebitInterestEntity entity)
        {
            var contract = new DebitInterest()
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

        public static List<DebitInterest> ToContract(this IEnumerable<DebitInterestEntity> DebitInterests)
        {
            return DebitInterests.Select(x => x.ToContract()).ToList();
        }

        public static List<DebitInterestEntity> ToEntity(this IEnumerable<DebitInterest> DebitInterests)
        {
            return DebitInterests.Select(x => x.ToEntity()).ToList();
        }

        public static DebitInterestEntity ToEntity(this DebitInterest contract, DebitInterestEntity entity = null)
        {
            var DebitInterestEntity = entity ?? new();

            DebitInterestEntity.Id= contract.Id;
DebitInterestEntity.Name= contract.Name;
DebitInterestEntity.InterestPercent= contract.InterestPercent;
DebitInterestEntity.Notes= contract.Notes;
DebitInterestEntity.IsDeleted= contract.IsDeleted;

            DebitInterestEntity.PaymentModeId= contract.PaymentModeId;
DebitInterestEntity.BrokerAgreementId= contract.BrokerAgreementId;

            return DebitInterestEntity;
        }     
    }
}

