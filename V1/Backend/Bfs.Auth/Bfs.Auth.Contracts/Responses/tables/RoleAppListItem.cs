using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleAppListItem
    {      
        public string? RoleApp_Id { get; set; }
public string? RoleApp_RoleId { get; set; }
public string? RoleApp_AppId { get; set; }

        public string? RoleName { get; set; }
public string? AppName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

