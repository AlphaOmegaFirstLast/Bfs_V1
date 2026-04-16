using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class TransactionListItem
    {      
        public string? Id { get; set; }
public string? Quantity { get; set; }
public string? Notes { get; set; }
public string? StoreId { get; set; }
public string? OperationId { get; set; }
public string? ProductId { get; set; }

        public string? StoreName { get; set; }
public string? OperationName { get; set; }
public string? ProductName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}