using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class CustomFieldDefinitionListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? FieldValidation { get; set; }
public string? DisplayName { get; set; }
public string? BfsComponentId { get; set; }

        public string? BfsComponentName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}