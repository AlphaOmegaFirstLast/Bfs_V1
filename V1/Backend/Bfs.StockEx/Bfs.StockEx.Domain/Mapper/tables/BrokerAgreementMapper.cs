using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class BrokerAgreementMapper
    {
        public static BrokerAgreement ToContract(this BrokerAgreementEntity entity)
        {
            var contract = new BrokerAgreement()
            {
               AgreementDate= entity.AgreementDate,
IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
OverdraftPrcnt= entity.OverdraftPrcnt,
OverdraftMx= entity.OverdraftMx,

               InvestorId= entity.InvestorId,
BrokerId= entity.BrokerId,
SsPortfolioId= entity.SsPortfolioId,

            };

            return contract;
        }

        public static List<BrokerAgreement> ToContract(this IEnumerable<BrokerAgreementEntity> BrokerAgreements)
        {
            return BrokerAgreements.Select(x => x.ToContract()).ToList();
        }

        public static List<BrokerAgreementEntity> ToEntity(this IEnumerable<BrokerAgreement> BrokerAgreements)
        {
            return BrokerAgreements.Select(x => x.ToEntity()).ToList();
        }

        public static BrokerAgreementEntity ToEntity(this BrokerAgreement contract, BrokerAgreementEntity entity = null)
        {
            var BrokerAgreementEntity = entity ?? new();

            BrokerAgreementEntity.AgreementDate= contract.AgreementDate;
BrokerAgreementEntity.IsDeleted= contract.IsDeleted;
BrokerAgreementEntity.Id= contract.Id;
BrokerAgreementEntity.Name= contract.Name;
BrokerAgreementEntity.Notes= contract.Notes;
BrokerAgreementEntity.OverdraftPrcnt= contract.OverdraftPrcnt;
BrokerAgreementEntity.OverdraftMx= contract.OverdraftMx;

            BrokerAgreementEntity.InvestorId= contract.InvestorId;
BrokerAgreementEntity.BrokerId= contract.BrokerId;
BrokerAgreementEntity.SsPortfolioId= contract.SsPortfolioId;

            return BrokerAgreementEntity;
        }     
    }
}

