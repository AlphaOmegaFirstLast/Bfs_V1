using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class DocumentDetailsListItem
    {      
        public string? Id { get; set; }
public string? Quantity { get; set; }
public string? Notes { get; set; }
public string? ProductId { get; set; }
public string? UnitId { get; set; }
public string? DocumentId { get; set; }

        public string? ProductName { get; set; }
public string? UnitName { get; set; }
public string? DocumentName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

