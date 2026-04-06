using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class UserRequestListItem
    {      
        public string? Id { get; set; }
public string? AspNetUserId { get; set; }
public string? Notes { get; set; }
public string? Name { get; set; }
public string? Email { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

