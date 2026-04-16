using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class CustomReportsListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Request { get; set; }
public string? BaseReport { get; set; }
public string? IsPrivate { get; set; }
public string? CreatedBy { get; set; }
public string? Url { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}