using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsField : IIdentifiable 
    {
        ///<Summary>
        /// BfsField IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BfsField ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// BfsField Field.
        ///</Summary>
        public string Field {get; set;} = string.Empty ;
///<Summary>
        /// BfsField DisplayName.
        ///</Summary>
        public string DisplayName {get; set;} = string.Empty ;

        ///<Summary>
        /// BfsField Component.
        ///</Summary>
        public long BfsComponentId {get; set;} = 0 ;
///<Summary>
        /// BfsField Filter Type.
        ///</Summary>
        public int FilterTypeId {get; set;} = 0 ;
///<Summary>
        /// BfsField Backend Type.
        ///</Summary>
        public int BackendDataTypeId {get; set;} = 0 ;

        ///<Summary>
        /// BfsField Field Validation.
        ///</Summary>
        public FieldValidation FieldValidation {get; set;} = new FieldValidation() ;
///<Summary>
        /// BfsField Report Info.
        ///</Summary>
        public ReportInfo ReportInfo {get; set;} = new ReportInfo() ;
///<Summary>
        /// BfsField Matrix Info.
        ///</Summary>
        public MatrixInfo MatrixInfo {get; set;} = new MatrixInfo() ;
///<Summary>
        /// BfsField ToolTip Info.
        ///</Summary>
        public ToolTipInfo ToolTipInfo {get; set;} = new ToolTipInfo() ;
///<Summary>
        /// BfsField Form Info.
        ///</Summary>
        public FormInfo FormInfo {get; set;} = new FormInfo() ;

    }
}

