using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class ComponentBusinessActionListItem
    {
    //Fields
        public bool IsDeleted { get; set; }
public long Id { get; set; }

        public long ComponentId { get; set; }
public long BusinessActionId { get; set; }
public int ActionLocationId { get; set; }

//Lookups
        public string? Component { get; set; }
public string? BusinessAction { get; set; }
public string? ActionLocation { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}