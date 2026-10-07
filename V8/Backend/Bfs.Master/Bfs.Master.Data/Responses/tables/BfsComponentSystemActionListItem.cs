using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsComponentSystemActionListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public long BfsComponentId { get; set; }
public long SystemActionId { get; set; }
public int ActionLocationId { get; set; }

        public string? BfsComponentName { get; set; }
public string? SystemActionName { get; set; }
public string? ActionLocationName { get; set; }

   }
}