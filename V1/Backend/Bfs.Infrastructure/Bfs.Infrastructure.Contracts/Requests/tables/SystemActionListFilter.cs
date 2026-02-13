using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class SystemActionListFilter
    {

        public string? Name { get; set; }
public string? MatchProprty { get; set; }
public string? MatchValues { get; set; }

        public int? ActionTypeId { get; set; }
public int? WriterTypeId { get; set; }

    }
}