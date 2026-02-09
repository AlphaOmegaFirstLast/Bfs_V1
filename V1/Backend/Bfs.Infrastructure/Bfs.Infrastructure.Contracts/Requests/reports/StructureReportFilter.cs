using Bfs.Core.Contracts;

namespace Bfs.Infrastructure.Contracts
{
    public class StructureReportFilter
    {

        public string? DisplayName { get; set; }

        public int? DataTypeId { get; set; }

        public NumericRange? countId { get; set; }

    }
}