using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class UserRequestListFilter
    {
        public long? Id { get; set; }
public long? UserId { get; set; }

        public string? AspNetUserId { get; set; }
public string? Name { get; set; }
public string? Email { get; set; }

        public long? UserRequestStatusId { get; set; }

        public DateRange? RequestDate { get; set; }
public DateRange? ResponseDate { get; set; }

    }
}