using Bfs.Core.Data;

namespace Bfs.Infrastructure.Data
{
    public class StructureReportItem
    {
        public int BfsComponentDataTypeId { get; set; }
public string BfsComponentDisplayName { get; set; }

        public string? DataTypeName { get; set; }

        public string? countId { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}