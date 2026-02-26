using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class BfsTenantListItem
    {      
        public string? DbConnection { get; set; }
public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? CustomFields { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}