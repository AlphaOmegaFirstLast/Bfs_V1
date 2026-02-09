using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class BfsFieldMapper
    {
        public static BfsField ToContract(this BfsFieldEntity entity)
        {
            var contract = new BfsField()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Field= entity.Field,
DisplayName= entity.DisplayName,
IsQueryColumn= entity.IsQueryColumn,
IsJoinField= entity.IsJoinField,
ParentTable= entity.ParentTable,

               BfsComponentId= entity.BfsComponentId,
FilterTypeId= entity.FilterTypeId,
BackendDataTypeId= entity.BackendDataTypeId,

               FieldValidation= entity.FieldValidation.ToContract(),
ReportInfo= entity.ReportInfo.ToContract(),
MatrixInfo= entity.MatrixInfo.ToContract(),
ToolTipInfo= entity.ToolTipInfo.ToContract(),
FormInfo= entity.FormInfo.ToContract(),

            };

            return contract;
        }

        public static List<BfsField> ToContract(this IEnumerable<BfsFieldEntity> BfsFields)
        {
            return BfsFields.Select(x => x.ToContract()).ToList();
        }

        public static List<BfsFieldEntity> ToEntity(this IEnumerable<BfsField> BfsFields)
        {
            return BfsFields.Select(x => x.ToEntity()).ToList();
        }

        public static BfsFieldEntity ToEntity(this BfsField contract, BfsFieldEntity entity = null)
        {
            var BfsFieldEntity = entity ?? new();

            BfsFieldEntity.IsDeleted= contract.IsDeleted;
BfsFieldEntity.Id= contract.Id;
BfsFieldEntity.Field= contract.Field;
BfsFieldEntity.DisplayName= contract.DisplayName;
BfsFieldEntity.IsQueryColumn= contract.IsQueryColumn;
BfsFieldEntity.IsJoinField= contract.IsJoinField;
BfsFieldEntity.ParentTable= contract.ParentTable;

            BfsFieldEntity.BfsComponentId= contract.BfsComponentId;
BfsFieldEntity.FilterTypeId= contract.FilterTypeId;
BfsFieldEntity.BackendDataTypeId= contract.BackendDataTypeId;

            BfsFieldEntity.FieldValidation= contract.FieldValidation.ToEntity();
BfsFieldEntity.ReportInfo= contract.ReportInfo.ToEntity();
BfsFieldEntity.MatrixInfo= contract.MatrixInfo.ToEntity();
BfsFieldEntity.ToolTipInfo= contract.ToolTipInfo.ToEntity();
BfsFieldEntity.FormInfo= contract.FormInfo.ToEntity();

            return BfsFieldEntity;
        }     
    }
}
