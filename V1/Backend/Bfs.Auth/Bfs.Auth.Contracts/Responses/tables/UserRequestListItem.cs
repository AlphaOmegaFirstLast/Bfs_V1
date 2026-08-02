using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class UserRequestListItem
    {      
        public string? UserRequest_Id { get; set; }
public string? UserRequest_Notes { get; set; }
public string? UserRequest_Name { get; set; }
public string? UserRequest_Email { get; set; }
public string? UserRequest_UserId { get; set; }
public string? UserRequest_RequestDate { get; set; }
public string? UserRequest_ResponseDate { get; set; }
public string? UserRequest_UserRequestStatusId { get; set; }

        public string? UserRequestStatusName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}