using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Data
{
    public class ClientListItem
    {
    //Fields
        public bool IsDeleted { get; set; }
public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public string DbConnection { get; set; }

        public string CustomFields { get; set; }

//Lookups

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}