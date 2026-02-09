using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsSystemListItem
    {      
        public string? BfsSystemId { get; set; }
public string? BfsSystemName { get; set; }
public string? BfsSystemNotes { get; set; }
public string? BfsSystemBfsClientId { get; set; }
public string? BfsSystemSystemTemplateId { get; set; }
public string? BfsSystemBasePortNumber { get; set; }
public string? BfsSystemDbPrefix { get; set; }

        public string? BfsClientName { get; set; }
public string? SystemTemplateName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}