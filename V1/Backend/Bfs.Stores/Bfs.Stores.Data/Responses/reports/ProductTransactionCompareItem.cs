using Bfs.Core.Data;

namespace Bfs.Stores.Data
{
    public class ProductTransactionCompareItem
    {
        public string? StrProduct_Name { get; set; }

        public string? sumQuantity { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}