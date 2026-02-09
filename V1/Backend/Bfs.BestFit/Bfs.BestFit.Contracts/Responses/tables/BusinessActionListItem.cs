using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class BusinessActionListItem
    {
        public bool IsDeleted { get; set; }
public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }

        public string? ActionType { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}