using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class UserRequestStatusListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

    }
}