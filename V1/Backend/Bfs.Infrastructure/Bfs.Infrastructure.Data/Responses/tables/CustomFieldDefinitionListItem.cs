using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class CustomFieldDefinitionListItem
    {      
        public string? CustomFieldDefinitionId { get; set; }
public string? CustomFieldDefinitionName { get; set; }
public string? CustomFieldDefinitionNotes { get; set; }
public string? CustomFieldDefinitionFieldValidation { get; set; }
public string? CustomFieldDefinitionDisplayName { get; set; }
public string? CustomFieldDefinitionBfsComponentId { get; set; }

        public string? BfsComponentName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}