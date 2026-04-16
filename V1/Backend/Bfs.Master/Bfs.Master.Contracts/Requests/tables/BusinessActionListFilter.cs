using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BusinessActionListFilter
    {

        public string? Name { get; set; }
public string? ShortName { get; set; }

        public int? ActionTypeId { get; set; }

    }
}