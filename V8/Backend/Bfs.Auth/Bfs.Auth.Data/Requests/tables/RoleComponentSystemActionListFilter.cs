using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class RoleComponentSystemActionListFilter
    {
        public bool? IsAutoComplete { get; set; } = false ;

        public long? Id { get; set; }

        public long? BfsComponentId { get; set; }
public long? SystemActionId { get; set; }
public long? RoleId { get; set; }

    }
}