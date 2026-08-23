using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class BusinessActionMapper
    {
        public static BusinessAction ToContract(this BusinessActionEntity entity)
        {
            var contract = new BusinessAction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
ShortName= entity.ShortName,
MatchProperty= entity.MatchProperty,
MatchValues= entity.MatchValues,
ActionTemplate= entity.ActionTemplate,
Name= entity.Name,
Notes= entity.Notes,

               ActionTypeId= entity.ActionTypeId,
WriterTypeId= entity.WriterTypeId,

            };

            return contract;
        }

        public static List<BusinessAction> ToContract(this IEnumerable<BusinessActionEntity> BusinessActions)
        {
            return BusinessActions.Select(x => x.ToContract()).ToList();
        }

        public static List<BusinessActionEntity> ToEntity(this IEnumerable<BusinessAction> BusinessActions)
        {
            return BusinessActions.Select(x => x.ToEntity()).ToList();
        }

        public static BusinessActionEntity ToEntity(this BusinessAction contract, BusinessActionEntity entity = null)
        {
            var BusinessActionEntity = entity ?? new();

            BusinessActionEntity.IsDeleted= contract.IsDeleted;
BusinessActionEntity.Id= contract.Id;
BusinessActionEntity.ShortName= contract.ShortName;
BusinessActionEntity.MatchProperty= contract.MatchProperty;
BusinessActionEntity.MatchValues= contract.MatchValues;
BusinessActionEntity.ActionTemplate= contract.ActionTemplate;
BusinessActionEntity.Name= contract.Name;
BusinessActionEntity.Notes= contract.Notes;

            BusinessActionEntity.ActionTypeId= contract.ActionTypeId;
BusinessActionEntity.WriterTypeId= contract.WriterTypeId;

            return BusinessActionEntity;
        }     
    }
}

