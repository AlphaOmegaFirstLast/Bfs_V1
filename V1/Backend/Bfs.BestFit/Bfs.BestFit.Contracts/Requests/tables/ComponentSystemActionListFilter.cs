using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class ComponentSystemActionListFilter
    {

        public long? ComponentId { get; set; }
public int? SystemActionId { get; set; }
public int? ActionLocationId { get; set; }

    }
}