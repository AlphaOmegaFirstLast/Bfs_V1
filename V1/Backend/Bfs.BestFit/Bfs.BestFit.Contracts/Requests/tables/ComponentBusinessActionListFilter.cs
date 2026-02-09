using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class ComponentBusinessActionListFilter
    {

        public long? ComponentId { get; set; }
public long? BusinessActionId { get; set; }
public int? ActionLocationId { get; set; }

    }
}