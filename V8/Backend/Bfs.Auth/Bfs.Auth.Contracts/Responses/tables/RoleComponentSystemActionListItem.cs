using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class RoleComponentSystemActionListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public long BfsComponentId { get; set; }
public long SystemActionId { get; set; }
public long RoleId { get; set; }

        public string? BfsComponentName { get; set; }
public string? SystemActionName { get; set; }
public string? RoleName { get; set; }

   }
}