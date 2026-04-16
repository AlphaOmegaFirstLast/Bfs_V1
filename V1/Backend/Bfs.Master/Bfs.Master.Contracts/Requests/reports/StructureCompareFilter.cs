using Bfs.Core.Contracts;

namespace Bfs.Master.Contracts
{
    public class StructureCompareFilter
    {

        public string? DisplayName { get; set; }

        public int? DataTypeId { get; set; }

        public NumericRange? countId { get; set; }

    }
}