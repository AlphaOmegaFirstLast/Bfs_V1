using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Data
{
    public class AppListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public long BfsSystemId { get; set; }
public string Logo { get; set; }

        public string? BfsSystemName { get; set; }

   }
}