using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class CustomFieldDefinitionListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public string DisplayName { get; set; }
public long BfsComponentId { get; set; }

        public dynamic FieldValidation { get; set; }
        public string? JsonFieldValidation { get; set; }

        public string? BfsComponentName { get; set; }

   }
}