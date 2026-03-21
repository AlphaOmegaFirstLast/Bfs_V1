using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthAppListFilter
    {

        public string? Name { get; set; }

        public long? BfsSystemId { get; set; }

    }
}