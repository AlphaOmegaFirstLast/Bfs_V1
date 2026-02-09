using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsComponentSystemActionListFilter
    {

        public long? BfsComponentId { get; set; }
public int? SystemActionId { get; set; }
public int? ActionLocationId { get; set; }

    }
}