using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthRoleUserListItem
    {      
        public string? Id { get; set; }
public string? AuthRoleId { get; set; }
public string? AuthUserId { get; set; }

        public string? AuthRoleName { get; set; }
public string? AuthUserName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}