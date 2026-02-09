using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BusinessActionListItem
    {      
        public string? BusinessActionId { get; set; }
public string? BusinessActionName { get; set; }
public string? BusinessActionNotes { get; set; }
public string? BusinessActionActionTypeId { get; set; }

        public string? ActionTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}