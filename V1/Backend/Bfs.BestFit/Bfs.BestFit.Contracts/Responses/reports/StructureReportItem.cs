using Bfs.Core.Contracts;

namespace Bfs.BestFit.Contracts
{
    public class StructureReportItem
    {
        public int ComponentDataTypeId { get; set; }
public string ComponentDisplayName { get; set; }

        public string? DataTypeName { get; set; }

        public string? countId { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}