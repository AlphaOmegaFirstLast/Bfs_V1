using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class CustomReportsListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string Name { get; set; }
public string Request { get; set; }
public string BaseReport { get; set; }
public bool IsPrivate { get; set; }
public string CreatedBy { get; set; }
public string Url { get; set; }

   }
}