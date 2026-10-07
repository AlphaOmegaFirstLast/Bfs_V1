using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class UserListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string AspNetUserId { get; set; }
public string Notes { get; set; }
public string Name { get; set; }
public string Email { get; set; }

   }
}