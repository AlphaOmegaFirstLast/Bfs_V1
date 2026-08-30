using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class SystemTemplateListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? ProjectType { get; set; }
public string? OutputDirectory { get; set; }
public string? SolutionDirectory { get; set; }
public string? Template { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}