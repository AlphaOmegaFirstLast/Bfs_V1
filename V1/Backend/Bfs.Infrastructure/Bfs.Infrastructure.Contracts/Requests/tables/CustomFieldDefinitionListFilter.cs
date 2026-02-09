using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class CustomFieldDefinitionListFilter
    {

        public string? Name { get; set; }

        public long? BfsComponentId { get; set; }

    }
}