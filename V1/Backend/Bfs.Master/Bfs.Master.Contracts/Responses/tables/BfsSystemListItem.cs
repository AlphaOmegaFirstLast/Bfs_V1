using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsSystemListItem
    {      
        public string? Id { get; set; }
public string? IsMaster { get; set; }
public string? Notes { get; set; }
public string? SystemTemplateId { get; set; }
public string? BasePortNumber { get; set; }
public string? DbPrefix { get; set; }
public string? Logo { get; set; }
public string? Name { get; set; }

        public string? SystemTemplateName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}