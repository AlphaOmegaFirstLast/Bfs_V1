using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class UserListFilter
    {
        public bool? IsAutoComplete { get; set; } = false;

        public long? Id { get; set; }

        public string? AspNetUserId { get; set; }
public string? Name { get; set; }
public string? Email { get; set; }

    }
}