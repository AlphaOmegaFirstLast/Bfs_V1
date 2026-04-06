using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class RoleAppListItem
    {      
        public string? Id { get; set; }
public string? RoleId { get; set; }
public string? AppId { get; set; }

        public string? RoleName { get; set; }
public string? AppName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

