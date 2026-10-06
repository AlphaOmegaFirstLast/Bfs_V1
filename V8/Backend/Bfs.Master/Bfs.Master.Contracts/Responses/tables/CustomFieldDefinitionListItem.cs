using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class CustomFieldDefinitionListItem
    {      
        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public string DisplayName { get; set; }
public long BfsComponentId { get; set; }

        public FieldValidation FieldValidation { get; set; }
        public string? JsonFieldValidation { get; set; }

        public string? BfsComponentName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}