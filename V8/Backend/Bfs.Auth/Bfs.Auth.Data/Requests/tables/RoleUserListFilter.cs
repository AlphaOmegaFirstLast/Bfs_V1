using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class RoleUserListFilter
    {
        public bool? IsAutoComplete { get; set; } = false ;

        public long? Id { get; set; }

        public long? RoleId { get; set; }
public long? UserId { get; set; }

    }
}