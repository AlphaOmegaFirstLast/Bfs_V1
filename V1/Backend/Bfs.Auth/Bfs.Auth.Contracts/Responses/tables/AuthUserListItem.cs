using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthUserListItem
    {      
        public string? Id { get; set; }
public string? AspNetUserId { get; set; }
public string? Notes { get; set; }
public string? Name { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

