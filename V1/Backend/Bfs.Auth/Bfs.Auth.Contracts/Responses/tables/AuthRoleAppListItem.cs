using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthRoleAppListItem
    {      
        public string? Id { get; set; }
public string? AuthRoleId { get; set; }
public string? AuthAppId { get; set; }

        public string? AuthRoleName { get; set; }
public string? AuthAppName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}