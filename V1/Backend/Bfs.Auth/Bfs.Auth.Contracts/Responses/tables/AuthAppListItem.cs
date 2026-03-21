using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthAppListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? BfsSystemId { get; set; }

        public string? BfsSystemName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}