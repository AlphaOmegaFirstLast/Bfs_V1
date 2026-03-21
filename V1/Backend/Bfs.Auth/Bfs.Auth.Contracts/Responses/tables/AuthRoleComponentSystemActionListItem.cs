using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthRoleComponentSystemActionListItem
    {      
        public string? Id { get; set; }
public string? BfsComponentId { get; set; }
public string? SystemActionId { get; set; }
public string? AuthRoleId { get; set; }

        public string? BfsComponentName { get; set; }
public string? SystemActionName { get; set; }
public string? AuthRoleName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}