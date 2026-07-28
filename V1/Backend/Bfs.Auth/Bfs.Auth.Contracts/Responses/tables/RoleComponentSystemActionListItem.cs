using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleComponentSystemActionListItem
    {      
        public string? RoleComponentSystemAction_Id { get; set; }
public string? RoleComponentSystemAction_BfsComponentId { get; set; }
public string? RoleComponentSystemAction_SystemActionId { get; set; }
public string? RoleComponentSystemAction_RoleId { get; set; }

        public string? BfsComponentName { get; set; }
public string? SystemActionName { get; set; }
public string? RoleName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

