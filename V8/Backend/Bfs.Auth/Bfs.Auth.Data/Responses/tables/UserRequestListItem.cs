using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class UserRequestListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string Notes { get; set; }
public string Name { get; set; }
public string Email { get; set; }
public long UserId { get; set; }
public DateTime RequestDate { get; set; }
public DateTime ResponseDate { get; set; }
public long UserRequestStatusId { get; set; }

        public string? UserRequestStatusName { get; set; }

   }
}