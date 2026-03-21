using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class BusinessActionMapper
    {
        public static BusinessAction ToContract(this BusinessActionEntity entity)
        {
            var contract = new BusinessAction()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
ShortName= entity.ShortName,

               ActionTypeId= entity.ActionTypeId,

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
BusinessActionEntity.Name= contract.Name;
BusinessActionEntity.Notes= contract.Notes;
BusinessActionEntity.ShortName= contract.ShortName;

            BusinessActionEntity.ActionTypeId= contract.ActionTypeId;

            return BusinessActionEntity;
        }     
    }
}

//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

