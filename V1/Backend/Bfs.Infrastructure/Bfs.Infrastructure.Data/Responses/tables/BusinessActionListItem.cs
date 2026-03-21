using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class BusinessActionListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? ActionTypeId { get; set; }
public string? ShortName { get; set; }

        public string? ActionTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

