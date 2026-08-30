using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class TransactionTypeListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? EffectTypeId { get; set; }
public string? StockEntityTypeId { get; set; }
public string? CalculationMethodId { get; set; }
public string? SourceTypeId { get; set; }
public string? StockFieldTypeId { get; set; }
public string? NextTransactionTypeId { get; set; }

        public string? EffectTypeName { get; set; }
public string? StockEntityTypeName { get; set; }
public string? CalculationMethodName { get; set; }
public string? SourceTypeName { get; set; }
public string? StockFieldTypeName { get; set; }
public string? NextTransactionTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

