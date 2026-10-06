using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class TransactionTypeListItem
    {      
        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public int EffectTypeId { get; set; }
public int StockEntityTypeId { get; set; }
public int CalculationMethodId { get; set; }
public int SourceTypeId { get; set; }
public int StockFieldTypeId { get; set; }
public int NextTransactionTypeId { get; set; }

        public string? EffectTypeName { get; set; }
public string? StockEntityTypeName { get; set; }
public string? CalculationMethodName { get; set; }
public string? SourceTypeName { get; set; }
public string? StockFieldTypeName { get; set; }
public string? NextTransactionTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}