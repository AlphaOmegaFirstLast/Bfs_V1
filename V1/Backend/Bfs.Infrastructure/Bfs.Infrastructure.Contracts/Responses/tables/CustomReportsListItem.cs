using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class CustomReportsListItem
    {      
        public string? CustomReportsId { get; set; }
public string? CustomReportsName { get; set; }
public string? CustomReportsRequest { get; set; }
public string? CustomReportsBaseReport { get; set; }
public string? CustomReportsIsPrivate { get; set; }
public string? CustomReportsCreatedBy { get; set; }
public string? CustomReportsUrl { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}