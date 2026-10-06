using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Master.Data.Models
{
    public class CustomFieldDefinitionEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public string DisplayName {get; set;} = string.Empty ;

        public long BfsComponentId {get; set;} = 0 ;

        public FieldValidation FieldValidation {get; set;} = new FieldValidation() ;

    }
}