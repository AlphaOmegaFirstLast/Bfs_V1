using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BusinessActionListFilter
    {
        public long? Id { get; set; }

        public string? ShortName { get; set; }
public string? MatchProperty { get; set; }
public string? MatchValues { get; set; }
public string? Name { get; set; }

        public int? ActionTypeId { get; set; }
public int? WriterTypeId { get; set; }

    }
}

