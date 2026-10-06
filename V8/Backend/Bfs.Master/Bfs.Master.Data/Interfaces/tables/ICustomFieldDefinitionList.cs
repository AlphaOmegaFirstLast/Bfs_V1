using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface ICustomFieldDefinitionList
    {
        Task<QueryResponse<CustomFieldDefinitionListItem>> GetAsync(QueryRequest<CustomFieldDefinitionListFilter> request);
    }
}