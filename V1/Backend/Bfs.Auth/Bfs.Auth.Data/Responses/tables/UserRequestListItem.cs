using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class UserRequestListItem
    {      
        public string? Id { get; set; }
public string? Notes { get; set; }
public string? Name { get; set; }
public string? Email { get; set; }
public string? UserId { get; set; }
public string? RequestDate { get; set; }
public string? ResponseDate { get; set; }
public string? UserRequestStatusId { get; set; }

        public string? UserRequestStatusName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

