using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class BfsManualCodeMapper
    {
        public static BfsManualCode ToContract(this BfsManualCodeEntity entity)
        {
            var contract = new BfsManualCode()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
FileName= entity.FileName,
StartTemplate= entity.StartTemplate,
EndTemplate= entity.EndTemplate,
Code= entity.Code,
Notes= entity.Notes,

               BfsSystemId= entity.BfsSystemId,
BfsComponentId= entity.BfsComponentId,

            };

            return contract;
        }

        public static List<BfsManualCode> ToContract(this IEnumerable<BfsManualCodeEntity> BfsManualCodes)
        {
            return BfsManualCodes.Select(x => x.ToContract()).ToList();
        }

        public static List<BfsManualCodeEntity> ToEntity(this IEnumerable<BfsManualCode> BfsManualCodes)
        {
            return BfsManualCodes.Select(x => x.ToEntity()).ToList();
        }

        public static BfsManualCodeEntity ToEntity(this BfsManualCode contract, BfsManualCodeEntity entity = null)
        {
            var BfsManualCodeEntity = entity ?? new();

            BfsManualCodeEntity.IsDeleted= contract.IsDeleted;
BfsManualCodeEntity.Id= contract.Id;
BfsManualCodeEntity.Name= contract.Name;
BfsManualCodeEntity.FileName= contract.FileName;
BfsManualCodeEntity.StartTemplate= contract.StartTemplate;
BfsManualCodeEntity.EndTemplate= contract.EndTemplate;
BfsManualCodeEntity.Code= contract.Code;
BfsManualCodeEntity.Notes= contract.Notes;

            BfsManualCodeEntity.BfsSystemId= contract.BfsSystemId;
BfsManualCodeEntity.BfsComponentId= contract.BfsComponentId;

            return BfsManualCodeEntity;
        }     
    }
}

