using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Data
{
    public class SystemInfoListItem
    {
        public bool IsDeleted { get; set; }
public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public string BasePortNumber { get; set; }

        public string? Client { get; set; }
public string? SystemTemplate { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}