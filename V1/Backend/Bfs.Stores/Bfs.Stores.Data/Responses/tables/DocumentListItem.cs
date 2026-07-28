using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Data
{
    public class DocumentListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? StoreId { get; set; }
public string? OperationId { get; set; }
public string? ResponseDate { get; set; }
public string? Notes { get; set; }

        public string? StoreName { get; set; }
public string? OperationName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

