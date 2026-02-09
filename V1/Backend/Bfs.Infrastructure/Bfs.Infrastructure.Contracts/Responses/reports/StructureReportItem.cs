using Bfs.Core.Contracts;

namespace Bfs.Infrastructure.Contracts
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