using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Infrastructure.Data.Models
{
    public class BfsFieldEntity : IIdentifiable, ITenanted
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Field {get; set;} = string.Empty ;
public string DisplayName {get; set;} = string.Empty ;
public bool IsQueryColumn {get; set;} = false ;
public bool IsJoinField {get; set;} = false ;
public string ParentTable {get; set;} = string.Empty ;

        public long BfsComponentId {get; set;} = 0 ;
public int FilterTypeId {get; set;} = 0 ;
public int BackendDataTypeId {get; set;} = 0 ;

        public FieldValidation FieldValidation {get; set;} = new FieldValidation() ;
public ReportInfo ReportInfo {get; set;} = new ReportInfo() ;
public MatrixInfo MatrixInfo {get; set;} = new MatrixInfo() ;
public ToolTipInfo ToolTipInfo {get; set;} = new ToolTipInfo() ;
public FormInfo FormInfo {get; set;} = new FormInfo() ;

    }
}