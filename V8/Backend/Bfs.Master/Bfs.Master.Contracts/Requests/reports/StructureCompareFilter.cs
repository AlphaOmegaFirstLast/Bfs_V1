using Bfs.Core.Contracts;

namespace Bfs.Master.Contracts
{
    public class StructureCompareFilter
    {

        public string? BfsComponent_DisplayName { get; set; }

        public int? BfsComponent_DataTypeId { get; set; }

        public NumericRange? countId { get; set; }

    }
}