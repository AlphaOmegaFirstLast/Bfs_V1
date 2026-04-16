using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AppListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }
public string? Logo { get; set; }

        public long? BfsSystemId { get; set; }

    }
}

