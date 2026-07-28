using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleListItem
    {      
        public string? Role_Id { get; set; }
public string? Role_Name { get; set; }
public string? Role_Notes { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

