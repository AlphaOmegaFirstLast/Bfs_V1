using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleUserListItem
    {      
        public string? Id { get; set; }
public string? RoleId { get; set; }
public string? UserId { get; set; }

        public string? RoleName { get; set; }

        public string? UserName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

