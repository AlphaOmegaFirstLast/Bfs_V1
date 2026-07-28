using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class RoleUserListItem
    {      
        public string? RoleUser_Id { get; set; }
public string? RoleUser_RoleId { get; set; }
public string? RoleUser_UserId { get; set; }

        public string? RoleName { get; set; }
public string? UserName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

