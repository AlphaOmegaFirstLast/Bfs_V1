using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleAppListFilter
    {
        public long? Id { get; set; }

        public long? RoleId { get; set; }
public long? AppId { get; set; }

    }
}