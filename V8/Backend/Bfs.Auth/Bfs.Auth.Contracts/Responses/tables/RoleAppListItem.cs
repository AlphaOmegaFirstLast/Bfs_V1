using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleAppListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public long RoleId { get; set; }
public long AppId { get; set; }

        public string? RoleName { get; set; }
public string? AppName { get; set; }

   }
}