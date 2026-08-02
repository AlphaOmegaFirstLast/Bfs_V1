using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class UserListFilter
    {
        public long? Id { get; set; }

        public string? AspNetUserId { get; set; }
public string? Name { get; set; }
public string? Email { get; set; }

    }
}