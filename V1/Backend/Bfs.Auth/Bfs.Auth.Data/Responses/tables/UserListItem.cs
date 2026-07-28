using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class UserListItem
    {      
        public string? User_Id { get; set; }
public string? User_AspNetUserId { get; set; }
public string? User_Notes { get; set; }
public string? User_Name { get; set; }
public string? User_Email { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

