using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class InvestorBrokerFundMapper
    {
        public static InvestorBrokerFund ToContract(this InvestorBrokerFundEntity entity)
        {
            var contract = new InvestorBrokerFund()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
Fund= entity.Fund,
FundDate= entity.FundDate,

               BrokerId= entity.BrokerId,
InvestorId= entity.InvestorId,

            };

            return contract;
        }

        public static List<InvestorBrokerFund> ToContract(this IEnumerable<InvestorBrokerFundEntity> InvestorBrokerFunds)
        {
            return InvestorBrokerFunds.Select(x => x.ToContract()).ToList();
        }

        public static List<InvestorBrokerFundEntity> ToEntity(this IEnumerable<InvestorBrokerFund> InvestorBrokerFunds)
        {
            return InvestorBrokerFunds.Select(x => x.ToEntity()).ToList();
        }

        public static InvestorBrokerFundEntity ToEntity(this InvestorBrokerFund contract, InvestorBrokerFundEntity entity = null)
        {
            var InvestorBrokerFundEntity = entity ?? new();

            InvestorBrokerFundEntity.IsDeleted= contract.IsDeleted;
InvestorBrokerFundEntity.Id= contract.Id;
InvestorBrokerFundEntity.Name= contract.Name;
InvestorBrokerFundEntity.Notes= contract.Notes;
InvestorBrokerFundEntity.Fund= contract.Fund;
InvestorBrokerFundEntity.FundDate= contract.FundDate;

            InvestorBrokerFundEntity.BrokerId= contract.BrokerId;
InvestorBrokerFundEntity.InvestorId= contract.InvestorId;

            return InvestorBrokerFundEntity;
        }     
    }
}

