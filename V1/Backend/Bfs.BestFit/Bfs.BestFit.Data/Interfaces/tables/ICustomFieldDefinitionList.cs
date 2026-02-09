using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface ICustomFieldDefinitionList
    {
        Task<QueryResponse<CustomFieldDefinitionListItem>> GetCustomFieldDefinitionListAsync(QueryRequest<CustomFieldDefinitionListFilter> request);
    }
}