using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Data
{
    public class StoreListItem
    {      
        public string? Store_Id { get; set; }
public string? Store_Name { get; set; }
public string? Store_Notes { get; set; }
public string? Store_AreaId { get; set; }

        public string? AreaName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

