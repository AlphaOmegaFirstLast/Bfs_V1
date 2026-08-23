using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class InvestorListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? Code { get; set; }
public string? Email { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}