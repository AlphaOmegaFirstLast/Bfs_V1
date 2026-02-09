using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class SystemActionListFilter
    {

        public string? Name { get; set; }

        public int? ActionTypeId { get; set; }

    }
}