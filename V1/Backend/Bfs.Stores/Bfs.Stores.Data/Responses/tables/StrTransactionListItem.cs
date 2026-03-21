using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Data
{
    public class StrTransactionListItem
    {      
        public string? Id { get; set; }
public string? Quantity { get; set; }
public string? Notes { get; set; }
public string? StrStoreId { get; set; }
public string? StrOperationId { get; set; }
public string? StrProductId { get; set; }

        public string? StrStoreName { get; set; }
public string? StrOperationName { get; set; }
public string? StrProductName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}