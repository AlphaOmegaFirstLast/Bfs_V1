using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Data
{
    public class ComponentListItem
    {
        public bool IsDeleted { get; set; }
public long Id { get; set; }
public bool IsSoftDelete { get; set; }
public string Name { get; set; }
public string DisplayName { get; set; }
public string MenuName { get; set; }
public string MenuPlaceHolder { get; set; }
public string Notes { get; set; }
public string QueryBaseTable { get; set; }

        public string? SystemInfo { get; set; }
public string? DataType { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}