using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsSystemListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public bool IsMaster { get; set; }
public string Notes { get; set; }
public int SystemTemplateId { get; set; }
public string BasePortNumber { get; set; }
public string DbPrefix { get; set; }
public string Logo { get; set; }
public string Name { get; set; }

        public string? SystemTemplateName { get; set; }

   }
}