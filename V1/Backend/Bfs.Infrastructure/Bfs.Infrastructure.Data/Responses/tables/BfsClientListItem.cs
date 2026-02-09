using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class BfsClientListItem
    {      
        public string? BfsClientDbConnection { get; set; }
public string? BfsClientId { get; set; }
public string? BfsClientName { get; set; }
public string? BfsClientNotes { get; set; }
public string? BfsClientCustomFields { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}