using Bfs.Core.Data;

namespace Bfs.Infrastructure.Data
{
    public class StructureCompareItem
    {
        public string? BfsComponent_DataTypeId { get; set; }
public string? BfsComponent_DisplayName { get; set; }

        public string? DataTypeName { get; set; }

        public string? countId { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}