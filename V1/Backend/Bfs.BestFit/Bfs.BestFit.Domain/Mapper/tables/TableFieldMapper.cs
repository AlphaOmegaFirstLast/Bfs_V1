using Bfs.Core.ObjectFields;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data.Models;

namespace Bfs.BestFit.Domain.Mapper
{
    public static class TableFieldMapper
    {
        public static TableField ToContract(this TableFieldEntity entity)
        {
            var contract = new TableField()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Field= entity.Field,
DisplayName= entity.DisplayName,
IsQueryColumn= entity.IsQueryColumn,
IsJoinField= entity.IsJoinField,
ParentTable= entity.ParentTable,
UiFormControlOrder= entity.UiFormControlOrder,

               ComponentId= entity.ComponentId,
FilterTypeId= entity.FilterTypeId,
BackendDataTypeId= entity.BackendDataTypeId,
FormControlTypeId= entity.FormControlTypeId,

               FieldValidation= entity.FieldValidation.ToContract(),
ReportInfo= entity.ReportInfo.ToContract(),
MatrixInfo= entity.MatrixInfo.ToContract(),
ToolTipInfo= entity.ToolTipInfo.ToContract(),
FormInfo= entity.FormInfo.ToContract(),

            };

            return contract;
        }

        public static List<TableField> ToContract(this IEnumerable<TableFieldEntity> TableFields)
        {
            return TableFields.Select(x => x.ToContract()).ToList();
        }

        public static List<TableFieldEntity> ToEntity(this IEnumerable<TableField> TableFields)
        {
            return TableFields.Select(x => x.ToEntity()).ToList();
        }

        public static TableFieldEntity ToEntity(this TableField contract, TableFieldEntity entity = null)
        {
            var TableFieldEntity = entity ?? new();

            TableFieldEntity.IsDeleted= contract.IsDeleted;
TableFieldEntity.Id= contract.Id;
TableFieldEntity.Field= contract.Field;
TableFieldEntity.DisplayName= contract.DisplayName;
TableFieldEntity.IsQueryColumn= contract.IsQueryColumn;
TableFieldEntity.IsJoinField= contract.IsJoinField;
TableFieldEntity.ParentTable= contract.ParentTable;
TableFieldEntity.UiFormControlOrder= contract.UiFormControlOrder;

            TableFieldEntity.ComponentId= contract.ComponentId;
TableFieldEntity.FilterTypeId= contract.FilterTypeId;
TableFieldEntity.BackendDataTypeId= contract.BackendDataTypeId;
TableFieldEntity.FormControlTypeId= contract.FormControlTypeId;

            TableFieldEntity.FieldValidation= contract.FieldValidation.ToEntity();
TableFieldEntity.ReportInfo= contract.ReportInfo.ToEntity();
TableFieldEntity.MatrixInfo= contract.MatrixInfo.ToEntity();
TableFieldEntity.ToolTipInfo= contract.ToolTipInfo.ToEntity();
TableFieldEntity.FormInfo= contract.FormInfo.ToEntity();

            return TableFieldEntity;
        }     
    }
}
