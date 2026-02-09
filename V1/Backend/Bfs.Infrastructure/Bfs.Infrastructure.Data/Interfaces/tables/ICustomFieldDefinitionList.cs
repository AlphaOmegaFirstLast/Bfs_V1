using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface ICustomFieldDefinitionList
    {
        Task<QueryResponse<CustomFieldDefinitionListItem>> GetAsync(QueryRequest<CustomFieldDefinitionListFilter> request);
    }
}