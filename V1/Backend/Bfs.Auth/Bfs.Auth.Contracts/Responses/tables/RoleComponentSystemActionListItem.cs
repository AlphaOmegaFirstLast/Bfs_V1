using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleComponentSystemActionListItem
    {      
        public string? Id { get; set; }
public string? BfsComponentId { get; set; }
public string? SystemActionId { get; set; }
public string? RoleId { get; set; }

        public string? BfsComponentName { get; set; }
public string? SystemActionName { get; set; }
public string? RoleName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

