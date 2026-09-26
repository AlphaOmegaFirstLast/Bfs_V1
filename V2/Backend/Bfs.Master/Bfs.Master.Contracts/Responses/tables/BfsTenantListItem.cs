using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsTenantListItem
    {      
        public string DbConnection { get; set; }
public long Id { get; set; }
public string Notes { get; set; }
public List<CustomField> CustomFields { get; set; }
public string Name { get; set; }
public string CompanyName { get; set; }
public string Logo { get; set; }
public string Theme { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}