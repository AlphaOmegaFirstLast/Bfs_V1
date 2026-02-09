using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsComponentBusinessActionListFilter
    {

        public long? BfsComponentId { get; set; }
public long? BusinessActionId { get; set; }
public int? ActionLocationId { get; set; }

    }
}