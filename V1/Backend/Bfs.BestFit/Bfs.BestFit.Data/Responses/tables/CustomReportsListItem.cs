using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Data
{
    public class CustomReportsListItem
    {
        public long Id { get; set; }
public string Name { get; set; }
public string Request { get; set; }
public string BaseReport { get; set; }
public bool IsPrivate { get; set; }
public bool IsDeleted { get; set; }
public string CreatedBy { get; set; }
public string Url { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}