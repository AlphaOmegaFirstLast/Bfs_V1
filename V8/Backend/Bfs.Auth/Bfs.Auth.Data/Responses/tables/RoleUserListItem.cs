using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class RoleUserListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public long RoleId { get; set; }
public long UserId { get; set; }

        public string? RoleName { get; set; }
public string? UserName { get; set; }

   }
}