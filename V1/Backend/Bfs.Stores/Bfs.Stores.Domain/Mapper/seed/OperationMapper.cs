using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class OperationMapper
    {
        public static Operation ToContract(this OperationEntity entity)
        {
            var contract = new Operation()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

               EffectTypeId= entity.EffectTypeId,
ThirdPartyTypeId= entity.ThirdPartyTypeId,

            };

            return contract;
        }

        public static List<Operation> ToContract(this IEnumerable<OperationEntity> Operations)
        {
            return Operations.Select(x => x.ToContract()).ToList();
        }

        public static List<OperationEntity> ToEntity(this IEnumerable<Operation> Operations)
        {
            return Operations.Select(x => x.ToEntity()).ToList();
        }

        public static OperationEntity ToEntity(this Operation contract, OperationEntity entity = null)
        {
            var OperationEntity = entity ?? new();

            OperationEntity.IsDeleted= contract.IsDeleted;
OperationEntity.Id= contract.Id;
OperationEntity.Name= contract.Name;
OperationEntity.Notes= contract.Notes;

            OperationEntity.EffectTypeId= contract.EffectTypeId;
OperationEntity.ThirdPartyTypeId= contract.ThirdPartyTypeId;

            return OperationEntity;
        }     
    }
}

