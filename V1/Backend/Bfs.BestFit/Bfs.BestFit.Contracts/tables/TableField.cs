using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class TableField : IIdentifiable
    {
        ///<Summary>
        /// TableField IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// TableField ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// TableField Field.
        ///</Summary>
        public string Field {get; set;} = string.Empty ;
///<Summary>
        /// TableField DisplayName.
        ///</Summary>
        public string DisplayName {get; set;} = string.Empty ;
///<Summary>
        /// TableField IsQueryColumn.
        ///</Summary>
        public bool IsQueryColumn {get; set;} = false ;
///<Summary>
        /// TableField IsJoinField.
        ///</Summary>
        public bool IsJoinField {get; set;} = false ;
///<Summary>
        /// TableField ParentTable.
        ///</Summary>
        public string ParentTable {get; set;} = string.Empty ;
///<Summary>
        /// TableField UIFormControlOrder.
        ///</Summary>
        public int UiFormControlOrder {get; set;} = 0 ;

        ///<Summary>
        /// TableField Component.
        ///</Summary>
        public long ComponentId {get; set;} = 0 ;
///<Summary>
        /// TableField Filter Type.
        ///</Summary>
        public int FilterTypeId {get; set;} = 0 ;
///<Summary>
        /// TableField Backend Type.
        ///</Summary>
        public int BackendDataTypeId {get; set;} = 0 ;
///<Summary>
        /// TableField Form Control Type.
        ///</Summary>
        public int FormControlTypeId {get; set;} = 0 ;

        ///<Summary>
        /// TableField Field Validation.
        ///</Summary>
        public FieldValidation FieldValidation {get; set;} = new FieldValidation() ;
///<Summary>
        /// TableField Report Info.
        ///</Summary>
        public ReportInfo ReportInfo {get; set;} = new ReportInfo() ;
///<Summary>
        /// TableField Matrix Info.
        ///</Summary>
        public MatrixInfo MatrixInfo {get; set;} = new MatrixInfo() ;
///<Summary>
        /// TableField ToolTip Info.
        ///</Summary>
        public ToolTipInfo ToolTipInfo {get; set;} = new ToolTipInfo() ;
///<Summary>
        /// TableField Form Info.
        ///</Summary>
        public FormInfo FormInfo {get; set;} = new FormInfo() ;

    }
}