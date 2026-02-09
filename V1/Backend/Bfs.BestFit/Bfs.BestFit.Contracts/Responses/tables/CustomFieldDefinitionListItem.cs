using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class CustomFieldDefinitionListItem
    {
        public bool IsDeleted { get; set; }
public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public string DisplayName { get; set; }

        public string? Component { get; set; }

        public string FieldValidation { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}