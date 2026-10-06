using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsComponentBusinessActionListFilter
    {
        public bool? IsAutoComplete { get; set; } = false ;

        public long? Id { get; set; }

        public long? BfsComponentId { get; set; }
public long? BusinessActionId { get; set; }
public int? ActionLocationId { get; set; }

    }
}