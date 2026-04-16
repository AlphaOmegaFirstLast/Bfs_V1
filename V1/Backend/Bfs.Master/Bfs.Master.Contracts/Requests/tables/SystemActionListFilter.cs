using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class SystemActionListFilter
    {

        public string? ShortName { get; set; }
public string? MatchProperty { get; set; }
public string? MatchValues { get; set; }
public string? Name { get; set; }

        public int? ActionTypeId { get; set; }
public int? WriterTypeId { get; set; }

    }
}