using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class SystemActionListItem
    {      
        public string? SystemActionId { get; set; }
public string? SystemActionName { get; set; }
public string? SystemActionNotes { get; set; }
public string? SystemActionActionTypeId { get; set; }

        public string? ActionTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}