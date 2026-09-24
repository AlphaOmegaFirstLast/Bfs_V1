using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsManualCodeListItem
    {      
        public long Id { get; set; }
public long BfsSystemId { get; set; }
public long BfsComponentId { get; set; }
public string Name { get; set; }
public string FileName { get; set; }
public string StartTemplate { get; set; }
public string EndTemplate { get; set; }
public string Code { get; set; }
public string Notes { get; set; }

        public string? BfsSystemName { get; set; }
public string? BfsComponentName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}