using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Data
{
    public class OperationListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? EffectTypeId { get; set; }
public string? ThirdPartyTypeId { get; set; }

        public string? EffectTypeName { get; set; }
public string? ThirdPartyTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

