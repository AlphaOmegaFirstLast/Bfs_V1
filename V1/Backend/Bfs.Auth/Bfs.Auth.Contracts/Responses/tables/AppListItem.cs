using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AppListItem
    {      
        public string? App_Id { get; set; }
public string? App_Name { get; set; }
public string? App_Notes { get; set; }
public string? App_BfsSystemId { get; set; }
public string? App_Logo { get; set; }

        public string? BfsSystemName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

