using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class UserRequestStatusListItem
    {      
        public string? UserRequestStatus_Id { get; set; }
public string? UserRequestStatus_Name { get; set; }
public string? UserRequestStatus_Notes { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}