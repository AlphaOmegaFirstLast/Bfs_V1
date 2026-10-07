using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class SystemTemplateListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public string ProjectType { get; set; }
public string OutputDirectory { get; set; }
public string SolutionDirectory { get; set; }
public string Template { get; set; }

   }
}