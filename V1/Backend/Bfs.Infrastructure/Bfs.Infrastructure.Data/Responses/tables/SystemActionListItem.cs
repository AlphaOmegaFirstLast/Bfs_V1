using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class SystemActionListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? ActionTypeId { get; set; }
public string? WriterTypeId { get; set; }
public string? MatchProprty { get; set; }
public string? MatchValues { get; set; }
public string? ActionTemplate { get; set; }

        public string? ActionTypeName { get; set; }
public string? WriterTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}