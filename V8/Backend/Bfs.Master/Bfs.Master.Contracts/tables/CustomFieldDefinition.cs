using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class CustomFieldDefinition : IIdentifiable 
    {
        ///<Summary>
        /// CustomFieldDefinition IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// CustomFieldDefinition ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// CustomFieldDefinition Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// CustomFieldDefinition Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// CustomFieldDefinition DisplayName.
        ///</Summary>
        public string DisplayName {get; set;} = string.Empty ;

        ///<Summary>
        /// CustomFieldDefinition Component.
        ///</Summary>
        public long BfsComponentId {get; set;} = 0 ;

        ///<Summary>
        /// CustomFieldDefinition Field Validation.
        ///</Summary>
        public FieldValidation FieldValidation {get; set;} = new FieldValidation() ;

    }
}